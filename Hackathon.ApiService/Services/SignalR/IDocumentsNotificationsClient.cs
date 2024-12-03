using Contracts;

namespace Hackathon.ApiService.Services.SignalR;

public interface IDocumentsNotificationsClient
{
    Task DocumentUploaded(Document document, CancellationToken cancellationToken = default);
}