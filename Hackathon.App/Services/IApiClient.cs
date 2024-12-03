using Contracts;
using Refit;

namespace Hackathon.App.Services;

public interface IApiClient
{
    [Multipart]
    [Post("/documents")]
    Task<Document> UploadAsync(StreamPart part, CancellationToken ct = default);
    
    [Get("/documents")]
    Task<IEnumerable<Document>> GetAsync(CancellationToken ct = default);
}