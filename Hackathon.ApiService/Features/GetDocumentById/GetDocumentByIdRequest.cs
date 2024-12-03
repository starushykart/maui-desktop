using Hackathon.ApiService.Persistence.Entities;
using MediatR;

namespace Hackathon.ApiService.Features.GetDocumentById;

public record GetDocumentByIdRequest(Guid Id) : IRequest<DocumentInfo?>;