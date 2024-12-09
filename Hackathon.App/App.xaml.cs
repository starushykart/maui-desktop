using CommunityToolkit.Mvvm.Messaging;
using Hackathon.App.Models;
using Hackathon.App.Models.Messages;
using Hackathon.App.Services.Background;
using Hackathon.App.Services.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Application = Microsoft.Maui.Controls.Application;

namespace Hackathon.App;

public partial class App : Application
{
    private readonly HubConnection _connection;
    private readonly OfflineTasksService _offlineService;
    private readonly SyncService _syncService;

    public static readonly string BaseApiUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2:5556" 
        : "http://localhost:5556";
    
    public App(OfflineTasksService service, SyncService syncService)
    {
        InitializeComponent();

        Preferences.Remove("SyncPath");

        _offlineService = service;
        _syncService = syncService;
        
        _connection = new HubConnectionBuilder()
            .WithUrl($"{BaseApiUrl}/documentsHub")
            .WithAutomaticReconnect(new InfiniteRetryPolicy())
            .Build();
        
        _connection.On<Document>("DocumentUpdated", doc =>
            WeakReferenceMessenger.Default.Send(new DocumentUpdated(doc)));
        
        Task.Run(() =>
        {
            Application.Current?.Dispatcher.Dispatch(async () =>
                await _connection.StartAsync());
        });
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell());
        
        _syncService.Start();
        _offlineService.Start();
        return window;
    }

    protected override async void CleanUp()
    {
        await _connection.DisposeAsync();
        _offlineService.Stop();
        _syncService.Stop();
        base.CleanUp();
    }
}