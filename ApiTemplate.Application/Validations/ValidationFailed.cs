using FluentValidation.Results;

namespace ApiTemplate.Application.Validations;

public class ValidationFailed
{
    public IEnumerable<ValidationFailure> ValidationErrors { get; }

    public ValidationFailed(IEnumerable<ValidationFailure> validationErrors)
    {
        ValidationErrors = validationErrors;
    }
}