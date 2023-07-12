using ApiTemplate.Application.Models;
using ApiTemplate.Application.Repositories;
using ApiTemplate.Application.Validations;
using ApiTemplate.Contracts.Requests;
using FluentValidation;
using MediatR;

namespace ApiTemplate.Commands;

public record UpdateUserCommand(Guid id, UpdateUserRequest user) : IRequest<Result<User?, ValidationFailed>>;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<User?, ValidationFailed>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<User> _validator;

    public UpdateUserHandler(IValidator<User> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task<Result<User?, ValidationFailed>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.id);
        if (user is null) throw new Exception();
        
        user.Name = request.user.Name;

        var validationResult = await _validator.ValidateAsync(user, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new ValidationFailed(validationResult.Errors);
        }

        await _userRepository.UpdateAsync(user);
        return user;
    }
}