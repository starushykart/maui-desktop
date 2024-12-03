using Contracts;
using MediatR;

namespace Hackathon.ApiService.Features.UploadDocument;

public record UploadDocumentCommand(IFormFile File): IRequest<Document>;