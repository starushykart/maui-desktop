using CommunityToolkit.Mvvm.Messaging;
using Hackathon.App.Models.Messages;
using Hackathon.App.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using Type = Hackathon.App.Models.Type;

namespace Hackathon.App.Services.Background;

public class OfflineTasksService(IApiClient apiClient)
{
    private Timer? _timer;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private bool _isOnline = true;

    public void Start()
    {
        _timer = new Timer(ExecuteTask!, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    private async void ExecuteTask(object state)
    {
        if (!await _semaphore.WaitAsync(0))
            return;

        try
        {
            var pingResult = await PingAsync();

            if (_isOnline != pingResult)
            {
                _isOnline = pingResult;
                WeakReferenceMessenger.Default.Send(new PingMessage(_isOnline));
            }

            if (pingResult == false)
                return;

            await using var context = new AppDbContext();
            var task = await context.OfflineTasks
                .AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (task == null)
                return;

            switch (task.Type)
            {
                case Type.None:
                    break;
                case Type.Upload:
                    break;
                case Type.Update:
                    break;
                case Type.Remove:
                    await apiClient.DeleteByIdAsync(task.DocumentId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await context.OfflineTasks
                .Where(x => x.Id == task.Id)
                .ExecuteDeleteAsync();
        }
        catch
        {
            // ignored
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task<bool> PingAsync()
    {
        try
        {
            await apiClient.PingAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Stop()
    {
        _semaphore.Dispose();
        _timer?.Dispose();
    }
}