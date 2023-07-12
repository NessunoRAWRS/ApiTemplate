using ApiTemplate.Application.Contexts;
using ApiTemplate.Application.Models;
using ApiTemplate.Application.Repositories;
using ApiTemplate.Application.Validations;
using ApiTemplate.Behaviors;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(c =>
    c.RegisterServicesFromAssemblyContaining<Program>()
        .AddOpenBehavior(typeof(LoggingBehavior<,>)));

builder.Services.AddScoped<IValidator<User>, UserValidator>();

builder.Services.AddDbContext<TemplateContext>(o => o.UseInMemoryDatabase("users"));

builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();