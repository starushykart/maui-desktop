namespace Hackathon.App.Pages;

public partial class OfflineTasksPage : ContentPage
{
    public OfflineTasksPage(OfflineTasksPageViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}