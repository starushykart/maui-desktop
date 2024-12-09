using Hackathon.AppHost.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var localstack = builder
    .AddLocalStack();

var postgres = builder
    .AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithPgWeb();

var database = postgres.AddDatabase("hackathonDb");

var apiService = builder
    .AddProject<Projects.Hackathon_ApiService>("api")
    .WithReference(database)
    .WaitFor(database)
    .WaitFor(localstack);

builder.Build().Run();