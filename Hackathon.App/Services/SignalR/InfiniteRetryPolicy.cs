using Microsoft.AspNetCore.SignalR.Client;

namespace Hackathon.App.Services.SignalR;

public class InfiniteRetryPolicy : IRetryPolicy
{
    public TimeSpan? NextRetryDelay(RetryContext retryContext)
        => TimeSpan.FromSeconds(1);
}