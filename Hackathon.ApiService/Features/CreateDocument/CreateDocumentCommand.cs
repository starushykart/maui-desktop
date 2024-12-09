using Hackathon.ApiService.Models;
using MediatR;

namespace Hackathon.ApiService.Features.CreateDocument;

public record CreateDocumentCommand(IFormFile File): IRequest<Document>;