namespace ApiTemplate.Application.Models;

public class User
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
}