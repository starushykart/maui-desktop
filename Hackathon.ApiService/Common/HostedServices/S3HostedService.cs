using Amazon.S3;
using Hackathon.ApiService.Common.Configuration;
using Microsoft.Extensions.Options;

namespace Hackathon.ApiService.Common.HostedServices;

public class S3HostedService(IAmazonS3 client, IOptions<S3Configuration> config, ILogger<S3HostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await client.PutBucketAsync(config.Value.Bucket, cancellationToken);
        logger.LogInformation("S3 bucket {Bucket} created", config.Value.Bucket);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}