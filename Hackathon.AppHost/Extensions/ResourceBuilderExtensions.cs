namespace Hackathon.AppHost.Extensions;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<ContainerResource> AddLocalStack(this IDistributedApplicationBuilder builder)
    {
        return builder
            .AddContainer("localstack", "localstack/localstack")
            .WithEndpoint(port: 4566, targetPort: 4566, scheme: "http", name: "default")
            .WithEnvironment("DEBUG", "1")
            .WithEnvironment("SERVICES", "s3");
    }
}