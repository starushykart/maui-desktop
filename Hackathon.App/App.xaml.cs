using Hackathon.App.Services.Abstractions;

namespace Hackathon.App;

public partial class App : Application
{
    public static string BaseApiUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2:5556" 
        : "http://localhost:5556";
    
    public App()
    {
        InitializeComponent();
        //trayService.Initialize();
        // trayService.ClickHandler = () =>
        //     ServiceProvider.GetService<INotificationService>()
        //         ?.ShowNotification("Hello Build! 😻 From .NET MAUI", "How's your weather?  It's sunny where we are 🌞");

    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}