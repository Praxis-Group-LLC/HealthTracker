using Domain.Coping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class CopingSessionConfiguration : IEntityTypeConfiguration<CopingSession>
{
    public void Configure(EntityTypeBuilder<CopingSession> builder)
    {
        builder.ToTable("coping_sessions");

        builder.HasKey(s => s.Id);
        
        builder.Property(c => c.Id)
            .ConfigureCopingSessionId()
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(s => s.UserId)
            .ConfigureUserId()
            .IsRequired();

        builder.Property(s => s.CopingExerciseId)
            .ConfigureCopingExerciseId()
            .IsRequired();

        builder.Property(s => s.StartedAt)
            .IsRequired();

        builder.Property(s => s.CompletedAt);

        builder.HasIndex(s => new { s.UserId, s.StartedAt });

        builder.HasOne<CopingExercise>()
            .WithMany()
            .HasForeignKey(s => s.CopingExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
