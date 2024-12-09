namespace Hackathon.App.Services.Background;

public class SyncService(IApiClient apiClient)
{
    private Timer? _timer;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    
    public void Start()
    {
        _timer = new Timer(ExecuteTask!, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
    }

    private async void ExecuteTask(object state)
    {
        
        if (!await _semaphore.WaitAsync(0))
            return;

        try
        {
            var syncPath = Preferences.Get("SyncPath", null);

            if (syncPath == null)
                return;
            
            var documents = await apiClient.GetAsync();

            foreach (var document in documents)
            {
                var filePath = Path.Combine(syncPath, document.Name);

                if (File.Exists(filePath))
                    continue;
                
                await using var fileStream = await apiClient.DownloadAsync(document.Id);
                await using var fileStreamToWrite = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await fileStream.CopyToAsync(fileStreamToWrite);
            }
        }
        catch(Exception ex)
        { }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Stop()
    {
        _semaphore.Dispose();
        _timer?.Dispose();
    }
}