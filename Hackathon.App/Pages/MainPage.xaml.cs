using Hackathon.App.Models;
using Hackathon.App.PageModels;

namespace Hackathon.App.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}