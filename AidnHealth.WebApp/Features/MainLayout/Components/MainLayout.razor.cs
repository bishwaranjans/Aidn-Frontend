using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace AidnHealth.WebApp.Features.MainLayout.Components;

public abstract class MainLayoutBase : LayoutComponentBase
{
    protected MudTheme Theme { get; } = new()
    {
        Typography = new Typography
        {
            Default = new Default
            {
                FontFamily = new[] { "soleto", "sans-serif" },
                FontWeight = 500
            },
            H1 = new H1
            {
                FontSize = "24px"
            },
            H2 = new H2
            {
                FontSize = "20px"
            },
            H3 = new H3
            {
                FontSize = "15px"
            }
        },
        Palette = new Palette
        {
            Primary = new MudColor("#005F7D"),
            Secondary = new MudColor("#00B5D8"),
            Tertiary = new MudColor("#B2E1EA"),
            AppbarBackground = new MudColor("#253747"),
            DrawerBackground = new MudColor("#EEEEE8")
        }
    };


}

