using System.Diagnostics.CodeAnalysis;
using BlazorTodoService.Features.Todos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService.Features.Authx;

public class AuthxDbContext : IdentityDbContext<AuthxUser, AuthxRole, Guid>
{
    private readonly ILogger<AuthxDbContext> _logger;

    public AuthxDbContext(ILogger<AuthxDbContext> logger, DbContextOptions<AuthxDbContext> options) :
        base(options) => _logger = logger;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public DbSet<Todo>? Todos { get; set; } = null;
}