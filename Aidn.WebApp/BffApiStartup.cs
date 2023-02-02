using Aidn.WebBffApi.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;

namespace Aidn.WebApp;

internal static class BffApiStartup
{
    public static void AddBffApiClients(this IServiceCollection services, Uri baseAddress)
    {
        services.AddClient<IAidnWebBffClient>(baseAddress);
    }

    private static void AddClient<T>(this IServiceCollection services, Uri baseAddress) where T : class
    {
        services.AddRefitClient<T>()
            .ConfigureHttpClient(x => x.BaseAddress = baseAddress)
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
    }
}
