using Aidn.Shared.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aidn.Shared.Models;

public class Measurement : IValidatableObject
{
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MeasurementType MeasurementType { get; set; }

    [Required]
    public int Value { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        switch (MeasurementType)
        {
            case MeasurementType.TEMP:
                if (Value < Constants.MinTemperature || Value > Constants.MaxTemperature)
                {
                    yield return new ValidationResult($"The field Value must be between {Constants.MinTemperature} and {Constants.MaxTemperature} for temperature measurements.", new[] { nameof(Value) });
                }
                break;
            case MeasurementType.HR:
                if (Value < Constants.MinHr || Value > Constants.MaxHr)
                {
                    yield return new ValidationResult($"The field Value must be between {Constants.MinHr} and {Constants.MaxHr} for heart rate measurements.", new[] { nameof(Value) });
                }
                break;
            case MeasurementType.RR:
                if (Value < Constants.MinRr || Value > Constants.MaxRr)
                {
                    yield return new ValidationResult($"The field Value must be between {Constants.MinRr} and {Constants.MaxRr} for respiratory rate measurements.", new[] { nameof(Value) });
                }
                break;
            default:
                break;
        }
    }
}

