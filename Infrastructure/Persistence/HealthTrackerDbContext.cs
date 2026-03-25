using Domain.Coping;
using Domain.DeviceCredentials;
using Domain.Journaling;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class HealthTrackerDbContext(DbContextOptions<HealthTrackerDbContext> options) : DbContext(options)
{
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<DeviceCredential> DeviceCredentials => Set<DeviceCredential>();
    public DbSet<CopingExercise> CopingExercises => Set<CopingExercise>();
    public DbSet<CopingSession> CopingSessions => Set<CopingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthTrackerDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}