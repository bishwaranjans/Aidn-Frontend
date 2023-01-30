using Microsoft.AspNetCore.Components;

namespace AidnHealth.WebApp.Features.Auth.Components;

public class AuthenticationBase : ComponentBase
{
    [Parameter] public string? Action { get; set; }
}

