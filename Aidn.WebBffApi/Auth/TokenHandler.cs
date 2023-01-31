using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Aidn.WebBffApi.Auth;

public class TokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _accessor;

    public TokenHandler(IHttpContextAccessor accessor) => _accessor = accessor;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_accessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

        // Set access token
        var accessToken = await _accessor.HttpContext.GetTokenAsync("access_token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
