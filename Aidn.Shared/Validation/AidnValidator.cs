using System.ComponentModel.DataAnnotations;

namespace Aidn.Shared.Validation;

public static class AidnValidator
{
    public static (bool isValid, List<ValidationResult> validationResults) TryValidateObjects(IEnumerable<object> objects)
    {
        var validationResults = new List<ValidationResult>();
        var isValid = true;

        foreach (var obj in objects)
        {
            if (obj is IValidatableObject validatableObject)
            {
                var objectValidationResults = validatableObject.Validate(new ValidationContext(validatableObject));
                validationResults.AddRange(objectValidationResults);
                isValid = isValid && !objectValidationResults.Any();
            }
            else
            {
                isValid = isValid && Validator.TryValidateObject(obj, new ValidationContext(obj), validationResults);
            }
        }

        return (isValid, validationResults);
    }
}

