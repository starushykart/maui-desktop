using MediatR;

namespace Hackathon.ApiService.Features.DownloadDocument;

public record DownloadDocumentCommand(Guid Id): IRequest<DownloadDocumentResponse>;