using Aidn.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;

namespace Aidn.WebApp.Features.Measurement;

public class NewsScoreBase : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = default!;

    protected List<Aidn.Shared.Measurement> Measurements = new()
    {
     new Shared.Measurement() { MeasurementType = MeasurementType.Temp, Value = 0 },
     new Shared.Measurement() { MeasurementType = MeasurementType.Hr, Value = 0 },
     new Shared.Measurement() { MeasurementType = MeasurementType.Rr, Value = 0 }
    };
    protected bool Success;
    protected string? ErrorMessage;
    protected string? Score;
    protected bool IsProcessing;
    protected async Task OnValidSubmit(EditContext context)
    {
        IsProcessing = true;
        try
        {
            var response = await HttpClient.PostAsJsonAsync("NewsScore", new MeasurementsModel(Measurements));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Success = true;
                Score = content;
            }
            else
            {
                ErrorMessage = "Error occurred.Try again!";
            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        finally
        {
            IsProcessing = false;
            StateHasChanged();
        }
    }
}

