using Hackathon.App.Models;
using Hackathon.App.Services.Persistence;
using Type = Hackathon.App.Models.Type;

namespace Hackathon.App.Services;

public class ApiClientService(IApiClient apiClient)
{
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await apiClient.DeleteByIdAsync(id);
        }
        catch
        {
            await using var context = new AppDbContext();
            context.OfflineTasks.Add(new OfflineTask
            {
                Type = Type.Remove,
                DocumentId = id
            });

            await context.SaveChangesAsync();
        }
    }
}