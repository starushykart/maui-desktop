using Hackathon.ApiService.Persistence.Entities;
using MediatR;

namespace Hackathon.ApiService.Features.GetAllDocuments;

public record GetAllDocumentsRequest : IRequest<List<DocumentInfo>>;