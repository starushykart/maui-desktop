using Hackathon.ApiService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.ApiService.Common.HostedServices;

public class MigrationHostedService(IServiceScopeFactory scopeFactory, ILogger<MigrationHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        await dbContext.Database.MigrateAsync(cancellationToken);
        
        logger.LogInformation("Database migrated");
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}