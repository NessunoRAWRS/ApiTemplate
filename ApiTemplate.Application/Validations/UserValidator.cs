using ApiTemplate.Application.Models;
using ApiTemplate.Contracts;
using FluentValidation;

namespace ApiTemplate.Application.Validations;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .MinimumLength(5);
    }
}