using MediatR;

namespace Hackathon.ApiService.Features.DeleteDocumentById;

public record DeleteDocumentByIdRequest(Guid Id) : IRequest;