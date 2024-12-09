namespace Hackathon.ApiService.Features.DownloadDocument;

public record DownloadDocumentResponse(Stream Stream, string FileName);