using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace AidnHealth.WebApp.Features.Auth.Components;

public class LoginDisplayBase : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    public void BeginLogOut()
    {
        NavigationManager.NavigateToLogout("authentication/logout");
    }
}

