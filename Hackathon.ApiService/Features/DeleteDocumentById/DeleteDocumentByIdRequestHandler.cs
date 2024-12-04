using Hackathon.ApiService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Features.DeleteDocumentById;

public class DeleteDocumentByIdRequestHandler(ApplicationDbContext context) : IRequestHandler<DeleteDocumentByIdRequest>
{
    public Task Handle(DeleteDocumentByIdRequest request, CancellationToken cancellationToken)
        => context.Documents
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);
}