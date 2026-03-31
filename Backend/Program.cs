using Application;
using Application.Abstractions;
using Application.Coping.Commands;
using Application.Coping.Queries;
using Application.Journaling.Commands;
using Application.Journaling.Queries;
using Application.Preferences.Commands;
using Application.Preferences.Queries;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend;
using Backend.Middleware;
using Backend.Security;
using Application.DTOs;
using Domain.Coping;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:8080");

// CORS ============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policyBuilder =>
            policyBuilder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
});

// Logging ============================
builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);

// Services ============================
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = DeviceTokenAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DeviceTokenAuthenticationDefaults.AuthenticationScheme;
    })
    .AddScheme<AuthenticationSchemeOptions, DeviceTokenAuthenticationHandler>(
        DeviceTokenAuthenticationDefaults.AuthenticationScheme,
        _ => { });

builder.Services.AddAuthorization();

// Service defaults & Aspire client integrations
builder.AddServiceDefaults();

builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

// Application layer DI ============================
builder.Services.AddHealthTrackerApplication();

// Connection string ============================
var connectionString = builder.Configuration.GetConnectionString("HealthTracker")
                       ?? throw new InvalidOperationException(
                           "Connection string 'HealthTracker' was not found. Set ConnectionStrings__HealthTracker " +
                           "or add it to Backend/appsettings.Development.json.");

// Infrastructure ============================
builder.Services.AddHealthTrackerInfrastructure(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.UseCors("CorsPolicy");

// HTTP request pipeline ============================
app.UseHealthTrackerExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Auth =============================
app.UseAuthentication();
app.UseAuthorization();

// API endpoints ============================
var api = app.MapGroup("/api");

// Coping endpoints ============================

api.MapGet("coping/exercises",
    async (IMediator mediator, CancellationToken ct) =>
        await mediator.Send(new GetCopingExercisesQuery(), ct)).WithName("GetCopingExercises");

api.MapGet("coping/sessions",
        async (DateTimeOffset from, DateTimeOffset to, IMediator mediator, ClaimsPrincipal user,
            CancellationToken ct) =>
        {
            var userId = user.GetUserId();

            var query = new GetCopingSessionsQuery(userId, from, to);
            return await mediator.Send(query, ct);
        })
    .WithName("GetCopingSessions")
    .RequireAuthorization();

api.MapPost("coping/sessions",
    async (StartCopingSessionRequest request, IMediator mediator, ClaimsPrincipal user, CancellationToken ct) =>
    {
        var userId = user.GetUserId();

        var cmd = new StartCopingSessionCommand(
            userId,
            request.ExerciseId,
            request.StartedAtUtc ?? DateTimeOffset.UtcNow);

        return await mediator.Send(cmd, ct);
    })
    .WithName("StartCopingSession")
    .RequireAuthorization();

api.MapPut($"coping/{{copingSessionId}}/complete", async (CopingSessionId copingSessionId,
        CompleteCopingSessionRequest request, IMediator mediator, CancellationToken ct) =>
    {
        var cmd = new CompleteCopingSessionCommand(copingSessionId, request.CompletedAtUtc ?? DateTimeOffset.UtcNow);
        await mediator.Send(cmd, ct);
        return Results.NoContent();
    })
    .WithName("CompleteCopingSession")
    .RequireAuthorization();

// Journal Entry endpoints ============================

api.MapPost("journal/entry",
        async (CreateJournalEntryRequest request, IMediator mediator, ClaimsPrincipal user, CancellationToken ct) =>
        {
            var userId = user.GetUserId();

            var cmd = new CreateJournalEntryCommand(
                userId,
                request.Date,
                request.Text,
                request.MoodRating,
                request.SleepHours,
                request.Tags);

            var id = await mediator.Send(cmd, ct);

            return Results.Created("GetRange", id);
        })
    .WithName("CreateJournalEntry")
    .RequireAuthorization();

api.MapGet("journal/getRange",
        async (DateOnly from, DateOnly to, IMediator mediator, ClaimsPrincipal user, CancellationToken ct) =>
        {
            var userId = user.GetUserId();
            var query = new GetJournalEntriesQuery(userId, from, to);
            return await mediator.Send(query, ct);
        })
    .WithName("GetJournalEntriesRange")
    .RequireAuthorization();

// User Preferences endpoints ============================

api.MapGet("user/preferences", async (IMediator mediator, ClaimsPrincipal user, CancellationToken ct) =>
    {
        var userId = user.GetUserId();
        var query = new GetUserPreferencesQuery(userId);
        return await mediator.Send(query, ct);
    })
    .WithName("GetUserPreferences")
    .RequireAuthorization();

api.MapPut("user/preferences",
        async (UpdatePreferencesRequest request, IMediator mediator, ClaimsPrincipal user, CancellationToken ct) =>
        {
            var userId = user.GetUserId();

            var cmd = new UpdateUserPreferencesCommand(
                userId,
                request.ScriptureModeEnabled);

            await mediator.Send(cmd, ct);
            return Results.NoContent();
        })
    .WithName("UpdateUserPreferences")
    .RequireAuthorization();

// Auth endpoints ============================

api.MapPost("auth/register-device", async (IDeviceAuthService deviceAuth, CancellationToken ct) =>
{
    var result = await deviceAuth.RegisterDeviceAsync(ct);

    var response = new DeviceRegistrationResponse(
        result.UserId.Value,
        result.DeviceToken);
    return response;
}).WithName("RegisterDevice");

// Health endpoint ==========================

api.MapGet("health/check", () => Task.FromResult(Results.Ok()));

// Finish Setup ============================

app.MapDefaultEndpoints();
app.UseFileServer();
app.MapFallbackToFile("index.html");

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<HealthTrackerDbContext>();

    await db.Database.MigrateAsync();

    var deviceAuthService = scope.ServiceProvider.GetRequiredService<IDeviceAuthService>();
    var deviceRegistrationResult = await deviceAuthService.RegisterDeviceAsync(CancellationToken.None);

    await DevelopmentDataSeeder.SeedAsync(db, deviceRegistrationResult);
}

app.Run();