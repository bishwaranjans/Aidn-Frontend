using Aidn.WebBffApi.Helpers;
using Aidn.WebBffApi.Settings;
using AidnMeasurementsApi.WebApi.Client;
using Refit;
using TokenHandler = Aidn.WebBffApi.Auth.TokenHandler;

namespace Aidn.WebBffApi.Startups;

internal static class AidnApiStartup
{
    public static void ConfigureClients(this IServiceCollection services, AppSettings settings)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<TokenHandler>();

        // Item API
        services.AddClient<IAidnMeasurementsApiClient>(settings.AidnMeasurementsApiUri!);
    }

    private static void AddClient<T>(this IServiceCollection services, Uri uri) where T : class
    {
        services.AddRefitClient<T>(RefitHelper.GetCustomRefitSettings())
            .ConfigureHttpClient(x => x.BaseAddress = uri)
            .AddHttpMessageHandler<TokenHandler>();
    }
}

