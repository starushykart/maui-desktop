using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hackathon.App.Models;
using Hackathon.App.Services.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.App.PageModels;

public partial class OfflineTasksPageViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isRefreshing;
    
    [ObservableProperty]
    private ObservableCollection<OfflineTask> _offlineTasks = [];

    [RelayCommand]
    private async Task Refresh()
    {
        await TaskUtilities.OnUIThreadAsync(() => IsRefreshing = true);
        
        try
        {
            await using var context = new AppDbContext();
            var tasks = await context.OfflineTasks.ToListAsync();
            await TaskUtilities.OnUIThreadAsync(() => OfflineTasks = new ObservableCollection<OfflineTask>(tasks));
        }
        finally
        {
            await TaskUtilities.OnUIThreadAsync(() => IsRefreshing = false);
        }
    }
}