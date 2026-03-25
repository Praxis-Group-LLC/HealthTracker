using Domain.DeviceCredentials;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class DeviceCredentialConfiguration : IEntityTypeConfiguration<DeviceCredential>
{
    public void Configure(EntityTypeBuilder<DeviceCredential> builder)
    {
        builder.ToTable("device_credentials");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ConfigureDeviceCredentialId()
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(c => c.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(c => c.TokenHash)
            .HasMaxLength(200)
            .HasColumnName("token_hash")
            .IsRequired();

        builder.Property(c => c.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(c => c.RevokedAtUtc)
            .HasColumnName("revoked_at_utc");

        builder.HasIndex(c => c.TokenHash)
            .IsUnique();
    }
}
