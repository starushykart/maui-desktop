using Hackathon.App.Models;
using Hackathon.App.Services.Persistence;
using Refit;
using Type = Hackathon.App.Models.Type;

namespace Hackathon.App.Services;

public class ApiClientService(IApiClient apiClient)
{
    public async Task DeleteAsync(Document document)
    {
        try
        {
            await apiClient.DeleteByIdAsync(document.Id);
        }
        catch
        {
            await using var context = new AppDbContext();
            
            context.OfflineTasks.Add(new OfflineTask
            {
                Type = Type.Remove,
                DocumentId = document.Id,
                DocumentName = document.Name,
                CreatedAt = DateTime.Now
            });

            await context.SaveChangesAsync();
        }
    }

    public async Task<Stream> DownloadAsync(Guid id)
        => await apiClient.DownloadAsync(id);

    public async Task<IEnumerable<Document>> GetAllAsync()
        => await apiClient.GetAsync();

    public async Task<Document> UploadAsync(FileResult file)
    {
        await using var stream = await file.OpenReadAsync();
        var streamPart = new StreamPart(stream, file.FileName, name: "file");
        return await apiClient.UploadAsync(streamPart);
    }
}