using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace Hackathon.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BindingContext = AppData.Instance;
        var currentTheme = Application.Current!.UserAppTheme;
        ThemeSegmentedControl.SelectedIndex = currentTheme == AppTheme.Light ? 0 : 1;
    }

    public static async Task DisplaySnackbarAsync(string message, Action<SnackbarOptions>? act = null)
    {
        var snackbarOptions = new SnackbarOptions
        {
            CornerRadius = new CornerRadius(5),
            Font = Font.SystemFontOfSize(18),
            ActionButtonFont = Font.SystemFontOfSize(14)
        };

        act?.Invoke(snackbarOptions);

        await Snackbar.Make(message, visualOptions: snackbarOptions).Show();
    }

    private void SfSegmentedControl_SelectionChanged(object sender,
        Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        Application.Current!.UserAppTheme = e.NewIndex == 0 ? AppTheme.Light : AppTheme.Dark;
    }
}