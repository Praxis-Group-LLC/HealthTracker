var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureContainerAppEnvironment("neuronest-aca")
    .WithDashboard(false);

var postgresServer = builder.AddAzurePostgresFlexibleServer("postgres");
var healthTrackerDb = postgresServer.AddDatabase("HealthTracker");

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithReference(healthTrackerDb)
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