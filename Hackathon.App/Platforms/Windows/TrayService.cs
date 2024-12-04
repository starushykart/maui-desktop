using Hardcodet.Wpf.TaskbarNotification.Interop;

namespace Hackathon.App.Platforms.Windows;

public class TrayService : ITrayService
{
    private WindowsTrayIcon _tray = null!;

    public void Initialize()
    {
        _tray = new WindowsTrayIcon(@"Platforms\Windows\trayicon.ico")
        {
            LeftClick = () => WindowExtensions.BringToFront()
        };
    }
}