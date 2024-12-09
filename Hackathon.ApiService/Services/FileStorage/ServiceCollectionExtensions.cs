using Amazon.S3;
using LocalStack.Client.Extensions;

namespace Hackathon.ApiService.Services.FileStorage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileStorage(this IServiceCollection services)
    {
        services.AddAWSServiceLocalStack<IAmazonS3>();
        services.AddSingleton<IFileStorage, FileStorage>();
        return services;
    }
}