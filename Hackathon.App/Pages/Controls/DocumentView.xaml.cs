using Hackathon.App.Models;

namespace Hackathon.App.Pages.Controls;

public partial class DocumentView
{
    public DocumentView()
    {
        InitializeComponent();
    }
    
    private void OnChecked(object sender, CheckedChangedEventArgs e)
    {
        var checkbox = (CheckBox)sender;

        if (checkbox.BindingContext is not Document document)
            return;

        document.IsChecked = e.Value;
    }
}