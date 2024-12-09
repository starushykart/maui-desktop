using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Hackathon.App.Models.Messages;

namespace Hackathon.App;

public partial class AppData : ObservableObject, IRecipient<PingMessage>
{
    public static AppData Instance { get; } = new();

    private AppData()
        => WeakReferenceMessenger.Default.Register(this);

    [ObservableProperty]
    private bool _isOnline = true;
    
    [ObservableProperty]
    private string? _syncPath;
    
    public async void Receive(PingMessage message)
    {
        await TaskUtilities.OnUIThreadAsync(async void () =>
        {
            if (message.IsOnline)
                await AppShell.DisplaySnackbarAsync("You are back online, Mr. White");
            else
                await AppShell.DisplaySnackbarAsync("You are not online, Mr. White. Offline mode activated");
            
            IsOnline = message.IsOnline;
        });
    }
}