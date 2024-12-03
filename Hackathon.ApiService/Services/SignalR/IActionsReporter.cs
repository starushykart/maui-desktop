using Contracts;

namespace Hackathon.ApiService.Services.SignalR;

public interface IActionsReporter
{
    Task NotifyDocumentUploadedAsync(Document document, CancellationToken ctx = default);
}