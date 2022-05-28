using FluentValidation.Results;

namespace ITEC.Backend.Application.Shared
{
    public class ValidationException : ApplicationException
    {
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public ValidationException(ValidationResult validationResult)
        {
            foreach (var err in validationResult.Errors)
            {
                ValidationErrors.Add(err.ErrorMessage);
            }
        }

        public ValidationException(string validationError)
        {
            ValidationErrors.Add(validationError);
        }

    }
}
