using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contracts;
using Microsoft.AspNetCore.SignalR.Client;
using Refit;

namespace Hackathon.App.PageModels;

public sealed partial class DocumentsPageViewModel : ObservableObject, IAsyncDisposable
{
    private readonly IApiClient _client;
    private readonly HubConnection _connection;
    
    private CancellationTokenSource? _uploadCancellationSource;

    [ObservableProperty]
    private ObservableCollection<Document> _documents = [];
    
    public DocumentsPageViewModel(IApiClient client)
    {
        _client = client;

        _connection = new HubConnectionBuilder()
            .WithUrl($"{App.BaseApiUrl}/documentsHub")
            .Build();

        _connection.On<Document>("DocumentUploaded", doc=> Documents.AddIfNotExist(doc));
        
        Task.Run(() =>
        {
            Application.Current?.Dispatcher.Dispatch(async () =>
                await _connection.StartAsync());
        });
    }
    
    [RelayCommand]
    private async Task Upload()
    {
        try
        {
            _uploadCancellationSource = new CancellationTokenSource();
            var fileResults = await FilePicker.Default.PickMultipleAsync();

            foreach (var fileResult in fileResults)
            {
                await using var stream = await fileResult.OpenReadAsync();
                var streamPart = new StreamPart(stream, fileResult.FileName, name: "file");
                var document = await _client.UploadAsync(streamPart, _uploadCancellationSource.Token);

                Documents.Add(document);
            }

            await AppShell.DisplayToastAsync("Files uploaded");
        }
        catch (OperationCanceledException)
        {
            await AppShell.DisplayToastAsync("Upload cancelled");
        }
        catch (Exception)
        {
            await AppShell.DisplayToastAsync("Files upload failed. Try again later.");
        }
        finally
        {
            _uploadCancellationSource?.Dispose();
            _uploadCancellationSource = null;
        }
    }

    [RelayCommand]
    private async Task Reload()
    {
        // var documents = await _client.GetAsync();
        // Documents = new ObservableCollection<Document>(documents);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        _uploadCancellationSource?.Dispose();
    }
}