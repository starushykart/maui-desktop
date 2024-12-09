using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Hackathon.App.Models;
using Hackathon.App.Models.Messages;

namespace Hackathon.App.PageModels;

public partial class DocumentsPageViewModel : 
    ObservableObject,
    IRecipient<DocumentUpdated>
{
    private readonly ApiClientService _service;
    
    [ObservableProperty]
    private ObservableCollection<Document> _documents = [];

    [ObservableProperty]
    private bool _isRefreshing;
    
    [ObservableProperty]
    private bool _isUploading;

    [ObservableProperty]
    private int _uploadProgress;

    public DocumentsPageViewModel(ApiClientService service)
    {
        _service = service;
        WeakReferenceMessenger.Default.Register(this);
    }
    
    [RelayCommand]
    private async Task Upload()
    {
        try
        {
            var fileResults = (await FilePicker.Default.PickMultipleAsync()).ToList();

            if (fileResults.Count == 0)
                return;
            
            await TaskUtilities.OnUIThreadAsync(() =>
            {
                IsUploading = true;
                UploadProgress = 0;
            });
            
            var divide = 100 / fileResults.Count;
            foreach (var fileResult in fileResults)
            {
                var document = await _service.UploadAsync(fileResult);
                
                await TaskUtilities.OnUIThreadAsync(() =>
                {
                    Documents.AddIfNotExist(document);
                    UploadProgress += divide;
                });
            }
        }
        catch (OperationCanceledException ex)
        {
            await AppShell.DisplaySnackbarAsync("Upload cancelled");
        }
        catch (Exception ex)
        {
            await AppShell.DisplaySnackbarAsync("Files upload failed. Try again later.");
        }
        finally
        {
            await TaskUtilities.OnUIThreadAsync(() =>
            {
                IsUploading = false;
                UploadProgress = 0;
            });
        }
    }
    
    [RelayCommand]
    private async Task Download(Document? document = null)
    {
        try
        {
           var result = await FolderPicker.Default.PickAsync();

            if (!result.IsSuccessful || result.Folder == null)
                return;

            var documents = document != null
                ? [document]
                : Documents.Where(x => x.IsChecked).ToList();

            foreach (var doc in documents)
            {
                var filePath = Path.Combine(result.Folder.Path, doc.Name);
                await using var fileStream = await _service.DownloadAsync(doc.Id);

                await using var fileStreamToWrite = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await fileStream.CopyToAsync(fileStreamToWrite);
            }
        }
        catch (OperationCanceledException ex)
        {
            await AppShell.DisplaySnackbarAsync("Upload cancelled");
        }
        catch (Exception ex)
        {
            await AppShell.DisplaySnackbarAsync("Files upload failed. Try again later.");
        }
        finally
        {
            IsUploading = false;
        }
    }
    
    [RelayCommand]
    private async Task Remove(Document document)
    {
        try
        {
            var confirmation = await Shell.Current.CurrentPage.DisplayAlert("Confirmation",
                "Are you sure you want to remove?", "Yes", "Cancel");
            
            if (!confirmation)
                return;
            
            await TaskUtilities.OnUIThreadAsync(async () =>
            {
                Documents.Remove(document);
                await AppShell.DisplaySnackbarAsync($"File {document.Name} deleted");
            });
            
            await _service.DeleteAsync(document);
        }
        catch
        { }
    }

    [RelayCommand]
    private async Task Refresh()
    {
        IsRefreshing = true;

        await Task.Delay(TimeSpan.FromSeconds(2));
        
        try
        {
            var documents = await _service.GetAllAsync();
            Documents = new ObservableCollection<Document>(documents);
        }
        catch (Exception ex)
        { }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task SetSyncFolder()
    {
        try
        {
            await TaskUtilities.OnUIThreadAsync(async () =>
            {
                var result = await FolderPicker.Default.PickAsync();
            
                if (!result.IsSuccessful || result.Folder == null)
                    return;
            
                Preferences.Set("SyncPath", result.Folder.Path);
                await AppShell.DisplaySnackbarAsync($"Folder {result.Folder.Name} selected as sync folder");
            });
            
        }
        catch
        { }
    }
    
    [RelayCommand]
    private void OpenSyncFolder()
    {
        try
        {
            var path = Preferences.Get("SyncPath", null);

            if (string.IsNullOrEmpty(path))
                return;
            
            Process.Start("open", path);
        }
        catch
        { }
    }

    public async void Receive(DocumentUpdated message)
    {
        var document = Documents.FirstOrDefault(x => x.Id == message.Document.Id);
        
        if (document == null)
        {
            await TaskUtilities.OnUIThreadAsync(() =>
            {
                Documents.AddIfNotExist(message.Document);
            });
        }
        else
        {
            await TaskUtilities.OnUIThreadAsync(() =>
            {
                document.Name = message.Document.Name;
            });
        }
    }
}