using ApiTemplate.Application.Models;
using ApiTemplate.Application.Repositories;
using ApiTemplate.Application.Validations;
using ApiTemplate.Contracts;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Commands;

public record CreateUserCommand(string Name) : IRequest<Result<User, ValidationFailed>>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Result<User, ValidationFailed>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _validator;

    public CreateUserHandler(IValidator<User> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task<Result<User, ValidationFailed>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        var validationResult = await _validator.ValidateAsync(user, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        await _userRepository.CreateAsync(user);
        return user;
    }
}