using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Backend;

public sealed class HealthTrackerDbContextFactory : IDesignTimeDbContextFactory<HealthTrackerDbContext>
{
    public HealthTrackerDbContext CreateDbContext(string[] args)
    {
        var backendPath = ResolveBackendPath();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(backendPath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("HealthTracker");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'HealthTracker' was not found for design-time DbContext creation. " +
                "Set ConnectionStrings__HealthTracker or add it to Backend/appsettings.Development.json.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<HealthTrackerDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new HealthTrackerDbContext(optionsBuilder.Options);
    }

    private static string ResolveBackendPath()
    {
        var current = Directory.GetCurrentDirectory();
        var candidates = new[]
        {
            current,
            Path.Combine(current, "Backend"),
            Path.GetFullPath(Path.Combine(current, "..", "Backend"))
        };

        foreach (var candidate in candidates)
        {
            if (File.Exists(Path.Combine(candidate, "appsettings.json")))
            {
                return candidate;
            }
        }

        return current;
    }
}
