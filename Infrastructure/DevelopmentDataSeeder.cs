using Application.Abstractions;
using Application.DTOs;
using Bogus;
using Domain.Coping;
using Domain.DeviceCredentials;
using Domain.Journaling;
using Domain.Shared;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DevelopmentDataSeeder
{
    private static readonly Faker Faker = new();
    private const int JournalEntryCount = 60;

    public static async Task SeedAsync(HealthTrackerDbContext db, DeviceRegistrationResult deviceAuthResult)
    {
        var seedUserId = UserId.New();
        var seedCopingExercise = CopingExercise.Create("BOXED_BREATHING", "Boxed Breathing",
            "A breathing exercise for mindfulness");

        if (!await db.AppUsers.AnyAsync())
        {
            db.AppUsers.Add(AppUser.CreateNew(seedUserId));
        }

        if (!await db.CopingExercises.AnyAsync())
        {
            await db.CopingExercises.AddAsync(CopingExercise.Create("BOXED_BREATHING", "Boxed Breathing",
                "A breathing exercise for mindfulness"));
        }

        if (!await db.CopingSessions.AnyAsync())
        {
            await db.CopingSessions.AddAsync(CopingSession.Start(seedUserId, seedCopingExercise.Id, DateTimeOffset.UtcNow));
        }

        if (!await db.JournalEntries.AnyAsync())
        {
            List<JournalEntry> seedJournalEntries = [];

            for (var i = 0; i < JournalEntryCount; i++)
            {
                seedJournalEntries.Add(JournalEntry.Create(seedUserId, new DateOnly(), Faker.Lorem.Sentence(),
                    Faker.Random.Int(), Faker.Random.Int(0, 24)));
            }

            await db.JournalEntries.AddRangeAsync(seedJournalEntries);
        }

        if (!await db.DeviceCredentials.AnyAsync())
        {
            await db.DeviceCredentials.AddAsync(DeviceCredential.CreateNew(seedUserId, deviceAuthResult.TokenHash,
                new DateTimeOffset()));
        }
    }
}