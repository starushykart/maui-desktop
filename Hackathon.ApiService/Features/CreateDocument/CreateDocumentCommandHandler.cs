using Hackathon.ApiService.Models;
using Hackathon.ApiService.Persistence;
using Hackathon.ApiService.Persistence.Entities;
using Hackathon.ApiService.Services.FileStorage;
using Hackathon.ApiService.Services.SignalR;
using MassTransit;
using MediatR;

namespace Hackathon.ApiService.Features.UploadDocument;

public class CreateDocumentCommandHandler(
    ApplicationDbContext context,
    IFileStorage storage,
    IActionsReporter actionsReporter)
    : IRequestHandler<UploadDocumentCommand, Document>
{
    public async Task<Document> Handle(UploadDocumentCommand request, CancellationToken cancellationToken)
    {
        await using var stream = request.File.OpenReadStream();

        var key = await storage.UploadAsync(stream, cancellationToken);

        var documentInfo = new DocumentInfo
        {
            Id = NewId.NextSequentialGuid(),
            Key = key,
            Name = request.File.FileName,
            Size = request.File.Length
        };

        context.Documents.Add(documentInfo);
        await context.SaveChangesAsync(cancellationToken);

        var document = new Document
        {
            Id = documentInfo.Id,
            Name = documentInfo.Name,
            Size = documentInfo.Size
        };

        await actionsReporter.NotifyDocumentUploadedAsync(document, cancellationToken);
        
        return document;
    }
}