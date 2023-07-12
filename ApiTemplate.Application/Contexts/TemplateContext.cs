using ApiTemplate.Application.Models;
using ApiTemplate.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Application.Contexts;

public class TemplateContext : DbContext
{
    public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
    {
    }
    
    public virtual DbSet<User> Users { get; set; }
}