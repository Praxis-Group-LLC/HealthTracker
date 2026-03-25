using Domain.Coping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class CopingExerciseConfiguration : IEntityTypeConfiguration<CopingExercise>
{
    private static readonly CopingExerciseId BoxBreathingId =
        new(Guid.Parse("11111111-1111-1111-1111-111111111111"));

    private static readonly CopingExerciseId GroundingId =
        new(Guid.Parse("22222222-2222-2222-2222-222222222222"));

    public void Configure(EntityTypeBuilder<CopingExercise> builder)
    {
        builder.ToTable("coping_exercises");

        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ConfigureCopingExerciseId()
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(e => e.Code)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.HasIndex(e => e.Code)
            .IsUnique();

        // Seed with stable GUID-backed IDs that match the strongly typed key.
        builder.HasData(
            new
            {
                Id = BoxBreathingId,
                Code = "BOX_BREATHING",
                Name = "Box Breathing",
                Description = "Inhale 4, hold 4, exhale 4, hold 4."
            },
            new
            {
                Id = GroundingId,
                Code = "GROUNDING_5_4_3_2_1",
                Name = "5-4-3-2-1 Grounding",
                Description = "Notice 5 things you see, 4 you feel, 3 you hear, 2 you smell, 1 you taste."
            }
        );
    }
}
