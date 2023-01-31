using Microsoft.AspNetCore.Components;

namespace AidnHealth.WebApp.Features.MainLayout.Components;

public class NavMenuBase : ComponentBase
{
    protected bool DrawerOpen = true;

    protected void DrawerToggle() => DrawerOpen = !DrawerOpen;
}

