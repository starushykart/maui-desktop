using Hackathon.ApiService.Models;
using Microsoft.AspNetCore.SignalR;

namespace Hackathon.ApiService.Services.SignalR;

public sealed class HubActionsReporter(IHubContext<DocumentsNotificationHub, IDocumentsNotificationsClient> hubContext) : IActionsReporter
{
    public async Task NotifyDocumentUploadedAsync(Document document, CancellationToken ctx = default)
        => await hubContext.Clients.All.DocumentUpdated(document, ctx);
}

