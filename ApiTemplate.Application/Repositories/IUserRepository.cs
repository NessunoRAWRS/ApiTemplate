using ApiTemplate.Application.Models;
using ApiTemplate.Contracts;

namespace ApiTemplate.Application.Repositories;

public interface IUserRepository
{
    public Task<User?> GetAsync(Guid id);
    public Task<User?> GetAsync(string id);
    public Task<IEnumerable<User>> GetAllAsync();
    public Task CreateAsync(User user);
    public Task UpdateAsync(User user);
}