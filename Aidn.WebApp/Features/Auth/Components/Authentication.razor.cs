using Microsoft.AspNetCore.Components;

namespace Aidn.WebApp.Features.Auth.Components;

public class AuthenticationBase : ComponentBase
{
    [Parameter] public string? Action { get; set; }
}

