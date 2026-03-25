using Application.DTOs;
using Domain.Shared;

namespace Application.Abstractions;

public interface IDeviceAuthService
{
    Task<DeviceRegistrationResult> RegisterDeviceAsync(CancellationToken ct = default);

    /// <summary>
    /// Validates a raw device token and returns the associated user id,
    /// or null if invalid / revoked.
    /// </summary>
    Task<UserId?> ValidateTokenAsync(string token, CancellationToken ct = default);
}