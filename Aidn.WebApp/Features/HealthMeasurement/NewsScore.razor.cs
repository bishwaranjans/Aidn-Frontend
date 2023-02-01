using Aidn.Shared.Validation;
using Aidn.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using Aidn.Shared.Helpers;

namespace Aidn.WebApp.Features.HealthMeasurement;

public class NewsScoreBase : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = default!;

    protected List<Measurement> Measurements = new()
    {
     new Measurement() { MeasurementType = MeasurementType.TEMP, Value = Constants.MinTemperature },
     new Measurement() { MeasurementType = MeasurementType.HR, Value = Constants.MinHr },
     new Measurement() { MeasurementType = MeasurementType.RR, Value = Constants.MinRr }
    };

    protected bool Success;
    protected List<string>? ValidationMessages;
    protected string? Score;
    protected bool IsProcessing;

    protected async Task OnValidSubmit(EditContext context)
    {
        IsProcessing = true;
        ValidationMessages = new();

        try
        {
            var (isValid, validationResults) = AidnValidator.TryValidateObjects(Measurements);

            if (!isValid)
            {
                foreach (var result in validationResults)
                {
                    ValidationMessages.Add(result.ErrorMessage!);
                }
            }
            else
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
                    ValidationMessages.Add("Error occurred.Try again!");
                }
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

