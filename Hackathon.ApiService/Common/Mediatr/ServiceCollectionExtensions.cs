using System.Reflection;

namespace Hackathon.ApiService.Common.Mediatr;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
        => services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
}