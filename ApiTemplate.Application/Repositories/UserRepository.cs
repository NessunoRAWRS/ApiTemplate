using ApiTemplate.Application.Contexts;
using ApiTemplate.Application.Models;
using ApiTemplate.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TemplateContext _db;

    public UserRepository(TemplateContext db)
    {
        _db = db;
    }

    public async Task<User?> GetAsync(Guid id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<User?> GetAsync(string id)
    {
        Guid.TryParse(id, out var guid);
        return await _db.Users.FindAsync(guid);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.Users.ToListAsync();
    }

    public async Task CreateAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
}