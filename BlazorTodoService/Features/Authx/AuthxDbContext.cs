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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _logger.LogInformation("AuthxDbContext#OnModelCreating");
        modelBuilder.ApplyConfiguration(new AuthxRoleConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public DbSet<Todo>? Todos { get; set; } = null;
}