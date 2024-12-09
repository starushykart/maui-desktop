using Hackathon.ApiService.Persistence;
using Hackathon.ApiService.Services.FileStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Features.DownloadDocument;

public class DownloadDocumentCommandHandler(
    ApplicationDbContext context,
    IFileStorage storage)
    : IRequestHandler<DownloadDocumentCommand, DownloadDocumentResponse>
{
    public async Task<DownloadDocumentResponse> Handle(DownloadDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await context
            .Documents
            .FirstAsync(x => x.Id == request.Id, cancellationToken);
        
        var stream = await storage.DownloadAsync(document.Key, cancellationToken);

        return new DownloadDocumentResponse(stream, document.Name);
    }
}