namespace Hackathon.ApiService.Services.FileStorage;

public interface IFileStorage
{
    Task<string> UploadAsync(Stream stream, CancellationToken ct);
}