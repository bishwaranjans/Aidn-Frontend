using Aidn.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Aidn.WebApp.Features.Measurement;

public class NewsScoreBase : ComponentBase
{
    protected Measurements model = new Measurements();
    protected bool success;

    protected void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
    }
}

