var builder = DistributedApplication.CreateBuilder(args);

var aca = builder.AddAzureContainerAppEnvironment("neuronest-aca")
    .WithDashboard(false);

var postgresServer = builder.AddAzurePostgresFlexibleServer("postgres");
var healthTrackerDb = postgresServer.AddDatabase("HealthTracker");

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithReference(healthTrackerDb)
    .WaitFor(healthTrackerDb)
    .WithHttpEndpoint(targetPort: 8080, name: "http")
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

builder.Build().Run();