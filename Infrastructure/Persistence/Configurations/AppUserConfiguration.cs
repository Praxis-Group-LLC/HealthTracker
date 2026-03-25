using Domain.Shared;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("app_users");

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value))
            .ValueGeneratedNever();

        builder.Property(u => u.CreatedAtUtc)
            .HasColumnName("created_at_utc");

        builder.Property(u => u.LastSeenAtUtc)
            .HasColumnName("last_seen_at_utc");
    }
}