using FluentValidation.Results;

namespace VehiculosAlquilerApp.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public List<string> Errors { get; set; } = [];

        public CustomValidationException(ValidationResult validationResult)
        {
            Errors.AddRange(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        public CustomValidationException(string errorMessage)
        {
            Errors.Add(errorMessage);
        }
    }
}
