using Hackathon.ApiService.Persistence;
using Hackathon.ApiService.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Features.GetDocumentById;

public class GetDocumentByIdRequestHandler(ApplicationDbContext context) : IRequestHandler<GetDocumentByIdRequest, DocumentInfo?>
{
    public Task<DocumentInfo?> Handle(GetDocumentByIdRequest request, CancellationToken cancellationToken)
        => context.Documents.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
}