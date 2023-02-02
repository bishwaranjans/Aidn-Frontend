using Aidn.Shared.Helpers;
using Aidn.Shared.Models;
using Aidn.Shared.Validation;
using Aidn.WebBffApi.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Aidn.WebApp.Features.HealthMeasurement;

public class NewsScoreBase : ComponentBase
{
    [Inject] private IAidnWebBffClient Client { get; set; } = default!;

    protected readonly List<Measurement> Measurements = new()
    {
     new Measurement() { MeasurementType = MeasurementType.TEMP, Value = Constants.MinTemperature },
     new Measurement() { MeasurementType = MeasurementType.HR, Value = Constants.MinHr },
     new Measurement() { MeasurementType = MeasurementType.RR, Value = Constants.MinRr }
    };

    protected bool Success;
    protected List<string>? ValidationMessages;
    protected string? Score;
    protected bool IsProcessing;

    protected async Task OnValidSubmit(EditContext _)
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
                var response = await Client.GetNewsScore(Measurements);

                if (response.IsSuccessStatusCode)
                {
                    Success = true;
                    Score = response.Content.ToString();
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

