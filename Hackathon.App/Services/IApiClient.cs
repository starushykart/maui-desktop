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

    [Get("/documents/{documentId}")]
    Task<Document?> GetByIdAsync(Guid documentId, CancellationToken ct = default);

    [Delete("/documents/{documentId}")]
    Task DeleteByIdAsync(Guid documentId, CancellationToken ct = default);

    [Patch("/documents/{documentId}")]
    Task PatchByIdAsync(Guid documentId, [Body] string? name, CancellationToken ct = default);
}