namespace Hackathon.App;

public partial class App : Application
{
    public static readonly string BaseApiUrl = DeviceInfo.Platform == DevicePlatform.Android
        ? "http://10.0.2.2:5556" 
        : "http://localhost:5556";
    
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}