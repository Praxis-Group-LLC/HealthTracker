// var builder = DistributedApplication.CreateBuilder(args);
//
// var postgres = builder.AddPostgres("postgres");
// var healthTrackerDb = postgres.AddDatabase("HealthTracker");
//
// var server = builder.AddProject<Projects.Backend>("backend")
//     .WithReference(healthTrackerDb)
//     .WaitFor(healthTrackerDb)
//     .WithHttpHealthCheck("/api/health/check")
//     .WithExternalHttpEndpoints();
//
// var webfrontend = builder.AddViteApp("webfrontend", "../frontend")
//     .WithReference(server)
//     .WaitFor(server);
//
// server.PublishWithContainerFiles(webfrontend, "wwwroot");
//
// builder.Build().Run();

var builder = DistributedApplication.CreateBuilder(args);

var aca = builder.AddAzureContainerAppEnvironment("neuronest-aca")
    .WithDashboard(false);

var postgres = builder.AddAzurePostgresFlexibleServer("postgres")
    .AddDatabase("HealthTracker");

var backend = builder.AddProject<Projects.Backend>("backend")
    .WithReference(postgres)
    .WithHttpEndpoint(targetPort: 8080, name: "http")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/api/health/check")
    .PublishAsAzureContainerApp((infrastructure, app) =>
    {
        // can leave empty for now
    });

builder.AddDockerfile("webfrontend", "../frontend")
    .WithHttpEndpoint(targetPort: 80)
    .WithEnvironment("VITE_API_BASE_URL", backend.GetEndpoint("http"))
    .WithExternalHttpEndpoints()
    .PublishAsAzureContainerApp((infrastructure, app) =>
    {
    });

builder.Build().Run();
