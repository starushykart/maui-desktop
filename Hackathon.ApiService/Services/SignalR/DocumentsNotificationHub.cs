using Microsoft.AspNetCore.SignalR;

namespace Hackathon.ApiService.Services.SignalR;

public class DocumentsNotificationHub(ILogger<DocumentsNotificationHub> logger) 
    : Hub<IDocumentsNotificationsClient>
{
    public override Task OnConnectedAsync()
    {
        logger.LogInformation("Client connected to hub");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogDebug("Client disconnected with {ConnectionId} from notifications", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}