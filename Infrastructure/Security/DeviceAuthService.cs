using System.Security.Cryptography;
using Domain.DeviceCredentials;
using Domain.Shared;
using Domain.Users;
using Application.Abstractions;
using Application.DTOs;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Security;

public sealed class DeviceAuthService(HealthTrackerDbContext dbContext, IDeviceTokenHasher hasher) : IDeviceAuthService
{
    public async Task<DeviceRegistrationResult> RegisterDeviceAsync(CancellationToken ct = default)
    {
        var now = DateTimeOffset.UtcNow;

        var user = AppUser.CreateNew(UserId.New());
        await dbContext.AppUsers.AddAsync(user, ct);

        var rawToken = GenerateToken();
        var tokenHash = hasher.HashToken(rawToken);

        var credential = DeviceCredential.CreateNew(user.UserId, tokenHash, now);
        await dbContext.DeviceCredentials.AddAsync(credential, ct);

        // Let UnitOfWorkBehavior / UoW handle SaveChanges,
        // but for registration endpoint we can also explicitly save if needed.
        await dbContext.SaveChangesAsync(ct);

        return new DeviceRegistrationResult(user.UserId, rawToken, tokenHash);
    }

    public async Task<UserId?> ValidateTokenAsync(string token, CancellationToken ct = default)
    {
        var tokenHash = hasher.HashToken(token);

        var cred = await dbContext.DeviceCredentials
            .AsNoTracking()
            .Where(c => c.TokenHash == tokenHash && c.RevokedAtUtc == null)
            .FirstOrDefaultAsync(ct);

        return cred?.UserId;
    }

    private static string GenerateToken()
    {
        // 32 bytes (256 bits) → high-entropy token, Base64Url-ish
        var bytes = new byte[32];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}
