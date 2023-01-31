using Aidn.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Aidn.WebApp.Features.Measurement;

public class NewsScoreBase : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = default!;

    protected List<Aidn.Shared.Measurement> model = new()
    {
     new Shared.Measurement() { MeasurementType = MeasurementType.Temp, Value = 0 },
     new Shared.Measurement() { MeasurementType = MeasurementType.Hr, Value = 0 },
     new Shared.Measurement() { MeasurementType = MeasurementType.Rr, Value = 0 }
    };
    protected bool success;
    protected string errorMessage;
    protected string? score;
    protected bool isProcessing;
    protected async Task OnValidSubmit(EditContext context)
    {
        isProcessing = true;
        try
        {
            var response = await HttpClient.PostAsJsonAsync("NewsScore", new MeasurementsModel(model));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                success = true;
                score = content;
            }
            else
            {
                errorMessage = "Error occurred.Try again!";
            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
        finally
        {
            isProcessing = false;
            StateHasChanged();
        }
    }
}

