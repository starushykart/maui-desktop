using Hackathon.ApiService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Features.PatchDocumentById;

public class PatchDocumentByIdRequestHandler(ApplicationDbContext context) : IRequestHandler<PatchDocumentByIdRequest>
{
    public async Task Handle(PatchDocumentByIdRequest request, CancellationToken cancellationToken)
    {
        var document = await context.Documents.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (request.Name != null)
            document!.Name = request.Name;

        await context.SaveChangesAsync(cancellationToken);
    }
}