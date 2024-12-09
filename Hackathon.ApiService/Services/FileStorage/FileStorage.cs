using Amazon.S3;
using Amazon.S3.Transfer;
using Hackathon.ApiService.Common.Configuration;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Hackathon.ApiService.Services.FileStorage;

public class FileStorage(IAmazonS3 client, IOptions<S3Configuration> config) : IFileStorage
{
    private readonly TransferUtility _utility = new(client);

    public async Task<string> UploadAsync(Stream stream, CancellationToken ct)
    {
        var key = NewId.NextSequentialGuid().ToString();
        await _utility.UploadAsync(stream, config.Value.Bucket, key, ct);

        return key;
    }
    
    public async Task<Stream> DownloadAsync(string key, CancellationToken ct)
    {
        var stream = await _utility.OpenStreamAsync(config.Value.Bucket, key, ct);

        if (stream == null)
            throw new FileNotFoundException();

        return stream;
    }
}