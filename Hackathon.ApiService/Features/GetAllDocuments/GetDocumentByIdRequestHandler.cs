using Hackathon.ApiService.Persistence;
using Hackathon.ApiService.Persistence.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Features.GetAllDocuments;

public class GetAllDocumentsRequestHandler(ApplicationDbContext context) : IRequestHandler<GetAllDocumentsRequest, List<DocumentInfo>>
{
    public Task<List<DocumentInfo>> Handle(GetAllDocumentsRequest request, CancellationToken cancellationToken)
        => context.Documents.ToListAsync(cancellationToken);
}