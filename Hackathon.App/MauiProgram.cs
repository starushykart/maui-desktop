using CommunityToolkit.Maui;
using Hackathon.App.Services.Background;
using Microsoft.Extensions.Logging;
using Refit;
using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Maui.Toolkit.Hosting;

namespace Hackathon.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWH1edHRSRWlcWExwWUo=");

        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionCore()
            .ConfigureSyncfusionToolkit()
            .ConfigureMauiHandlers(handlers => { })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SegoeUI-Semibold.ttf", "SegoeSemibold");
                fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
            });

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddLogging(configure => configure.AddDebug());
#endif

        builder.Services
            .AddSingleton<ApiClientService>()
            .AddRefitClient<IApiClient>()
            .ConfigureHttpClient(x => x.BaseAddress = new Uri(App.BaseApiUrl));

        builder.Services.AddSingleton<OfflineTasksService>();
        builder.Services.AddSingleton<SyncService>();

        builder.Services.AddSingleton<DocumentsPageViewModel>();
        builder.Services.AddSingleton<ProjectRepository>();
        builder.Services.AddSingleton<TaskRepository>();
        builder.Services.AddSingleton<CategoryRepository>();
        builder.Services.AddSingleton<TagRepository>();
        builder.Services.AddSingleton<SeedDataService>();
        builder.Services.AddSingleton<ModalErrorHandler>();
        builder.Services.AddSingleton<MainPageModel>();
        builder.Services.AddSingleton<ProjectListPageModel>();
        builder.Services.AddSingleton<OfflineTasksPageViewModel>();

        builder.Services.AddSingleton<ManageMetaPageModel>();

        builder.Services.AddSingletonWithShellRoute<DocumentsPage, DocumentsPageViewModel>("documents");
        builder.Services.AddSingletonWithShellRoute<OfflineTasksPage, OfflineTasksPageViewModel>("offline-tasks");
        builder.Services.AddTransientWithShellRoute<TaskDetailPage, TaskDetailPageModel>("task");

#if MACCATALYST
        AddMacPlatformSpecificServices(builder);
#elif WINDOWS
        AddWindowsPlatformSpecificServices(builder);
#endif
        return builder.Build();
    }
    
    private static void AddWindowsPlatformSpecificServices(MauiAppBuilder? builder)
    {
        if (builder == null)
            return;
    }

    private static void AddMacPlatformSpecificServices(MauiAppBuilder? builder)
    {
        if (builder == null)
            return;
    }
}