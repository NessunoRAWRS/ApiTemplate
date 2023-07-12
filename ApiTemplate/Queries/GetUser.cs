using ApiTemplate.Application.Models;
using ApiTemplate.Application.Repositories;
using ApiTemplate.Contracts;
using MediatR;

namespace ApiTemplate.Queries;

public record GetUserQuery(Guid Id) : IRequest<User?>;

public class GetUserHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAsync(request.Id);
    }
}