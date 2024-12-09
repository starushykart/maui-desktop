using Hackathon.ApiService.Models;

namespace Hackathon.ApiService.Services.SignalR;

public interface IDocumentsNotificationsClient
{
    Task DocumentUpdated(Document document, CancellationToken cancellationToken = default);
}