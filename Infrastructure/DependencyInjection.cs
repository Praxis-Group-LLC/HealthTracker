using Application.Abstractions;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers Infrastructure services. Caller provides DB options so we stay provider-agnostic.
    /// </summary>
    public static IServiceCollection AddHealthTrackerInfrastructure(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbOptions)
    {
        services.AddDbContext<HealthTrackerDbContext>(dbOptions);

        services.AddScoped<IJournalEntryRepository, JournalEntryRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        services.AddScoped<ICopingExerciseRepository, CopingExerciseRepository>();
        services.AddScoped<ICopingSessionRepository, CopingSessionRepository>();
        services.AddScoped<IDeviceTokenHasher, DeviceTokenHasher>();
        services.AddScoped<IDeviceAuthService, DeviceAuthService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}