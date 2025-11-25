using MyWebApi.Repositories;
using MyWebApi.Services;

namespace MyWebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register Repositories
        services.AddScoped<IWeatherRepository, WeatherRepository>();
        services.AddScoped<IBlobRepository, BlobRepository>();
        services.AddScoped<IEmailConfigRepository, EmailConfigRepository>();

        // Register Services
        services.AddSingleton<IIdHasher, IdHasher>();
        services.AddScoped<IHttpService, HttpService>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IBackgroundJobService, BackgroundJobService>();

        return services;
    }
}
