var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureContainerAppEnvironment("xenonest-aca")
    .WithDashboard(false);

var keyVault = builder.AddAzureKeyVault("kv");

var pgUser = builder.AddParameter("pg-user", secret: true);
var pgPassword = builder.AddParameter("pg-password", secret: true);

var pgPasswordSecret = keyVault.AddSecret("kv-pg-password", pgPassword);
var pgUserSecret = keyVault.AddSecret("kv-pg-user", pgUser);

var postgresServer = builder.AddAzurePostgresFlexibleServer("postgres")
    .WithPasswordAuthentication(pgUser, pgPassword);

var healthTrackerDb = postgresServer.AddDatabase("HealthTracker");

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithReference(healthTrackerDb)
    .WithReference(keyVault)
    .WaitFor(healthTrackerDb)
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/api/health/check")
    .PublishAsAzureContainerApp((infrastructure, app) =>
    {
        // can leave empty for now
    });

var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
    .WithReference(backend)
    .WaitFor(backend);

backend.PublishWithContainerFiles(webfrontend, "wwwroot");

await builder.Build().RunAsync();