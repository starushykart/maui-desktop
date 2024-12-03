namespace Hackathon.ApiService.Common.HostedServices;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddHostedServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHostedService<MigrationHostedService>()
            .AddHostedService<S3HostedService>();

        return builder;
    }
}