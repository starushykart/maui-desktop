using CommunityToolkit.Mvvm.ComponentModel;

namespace Hackathon.App.Models;

public partial class Document : ObservableObject
{
    [ObservableProperty]
    private Guid _id;

    [ObservableProperty]
    private string _name = null!;

    [ObservableProperty]
    private long _size;
    
    [ObservableProperty]
    private bool _isChecked;
}