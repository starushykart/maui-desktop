using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Refit;
using Syncfusion.Maui.Toolkit.Hosting;

namespace Hackathon.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureMauiHandlers(handlers => { })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
            });


        builder.ConfigureLifecycleEvents(lifecycle =>
        {
#if WINDOWS
            lifecycle.AddWindows(lifecycleBuilder => lifecycleBuilder.OnWindowCreated(window =>
            {
                window.ExtendsContentIntoTitleBar = false;
                var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);

                Platforms.Windows.WindowExtensions.Hwnd = handle;

                var trayService = new Platforms.Windows.TrayService();
                trayService.Initialize();

                appWindow.Closing += async (_, e) =>
                {
                    var mainPage = Application.Current?.Windows[0].Page;
                    if (mainPage == null)
                        return;

                    e.Cancel = true;

                    var result = await mainPage.DisplayAlert(
                        "Closing the application",
                        "Do you really want to quit?",
                        "Close",
                        "Minimize to system tray")!;

                    if (result)
                        Application.Current!.Quit();

                    Platforms.Windows.WindowExtensions.MinimizeToTray();
                };
            }));
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddLogging(configure => configure.AddDebug());
#endif

        builder.Services.AddRefitClient<IApiClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(App.BaseApiUrl));

        builder.Services.AddSingleton<DocumentsPageViewModel>();
        builder.Services.AddSingleton<ProjectRepository>();
        builder.Services.AddSingleton<TaskRepository>();
        builder.Services.AddSingleton<CategoryRepository>();
        builder.Services.AddSingleton<TagRepository>();
        builder.Services.AddSingleton<SeedDataService>();
        builder.Services.AddSingleton<ModalErrorHandler>();
        builder.Services.AddSingleton<MainPageModel>();
        builder.Services.AddSingleton<ProjectListPageModel>();
        builder.Services.AddSingleton<ManageMetaPageModel>();

        builder.Services.AddTransientWithShellRoute<ProjectDetailPage, ProjectDetailPageModel>("project");
        builder.Services.AddTransientWithShellRoute<TaskDetailPage, TaskDetailPageModel>("task");

#if MACCATALYST
        AddMacPlatformSpecificServices(builder);
#endif

        return builder.Build();
    }

    private static void AddMacPlatformSpecificServices(MauiAppBuilder? builder)
    {
    }
}