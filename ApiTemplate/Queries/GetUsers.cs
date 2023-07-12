using ApiTemplate.Application.Models;
using ApiTemplate.Application.Repositories;
using MediatR;

namespace ApiTemplate.Queries;

public record GetUsersQuery() : IRequest<IEnumerable<User>>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllAsync();
    }
}