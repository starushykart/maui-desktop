using Hackathon.ApiService.Models;
using Hackathon.ApiService.Persistence;
using Hackathon.ApiService.Services.SignalR;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Features.PatchDocumentById;

public class PatchDocumentByIdRequestHandler(ApplicationDbContext context, IActionsReporter actionsReporter)
    : IRequestHandler<PatchDocumentByIdRequest>
{
    public async Task Handle(PatchDocumentByIdRequest request, CancellationToken cancellationToken)
    {
        var documentInfo = await context.Documents.FirstAsync(x => x.Id == request.Id, cancellationToken);

        if (request.Name != null)
            documentInfo.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);

        var document = new Document
        {
            Id = documentInfo.Id,
            Name = documentInfo.Name,
            Size = documentInfo.Size
        };

        await actionsReporter.NotifyDocumentUploadedAsync(document, cancellationToken);
    }
}