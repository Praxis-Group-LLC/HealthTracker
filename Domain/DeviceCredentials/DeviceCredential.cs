using Domain.Shared;

namespace Domain.DeviceCredentials
{
    public class DeviceCredential
    {
        public DeviceCredentialId Id { get; init; }
        public UserId UserId { get; init; }
        public string TokenHash { get; init; } = null!;
        public DateTimeOffset CreatedAtUtc { get; init; }
        public DateTimeOffset? RevokedAtUtc { get; init; }
        
        // ReSharper disable once UnusedMember.Local
        private DeviceCredential()
        {
        }

        private DeviceCredential(UserId userId, string tokenHash, DateTimeOffset now)
        {
            Id = DeviceCredentialId.New();
            UserId = userId;
            TokenHash = tokenHash;
            CreatedAtUtc = now;
        }

        public static DeviceCredential CreateNew(UserId userId, string tokenHash, DateTimeOffset now)
        {
            return new DeviceCredential(userId, tokenHash, now);
        }
    }
}
