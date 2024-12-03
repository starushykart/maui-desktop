namespace Hackathon.App.Pages;

public partial class DocumentsPage : ContentPage
{
    public DocumentsPage(DocumentsPageViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}