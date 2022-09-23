using System.Reflection;
using BlazorTodoService.Features.Authx;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService;

public partial class BlazorTodoDbContext : IdentityDbContext<AuthxUser, AuthxRole, Guid>
{
    private readonly ILogger<BlazorTodoDbContext> _logger;

    public BlazorTodoDbContext(ILogger<BlazorTodoDbContext> logger, DbContextOptions<BlazorTodoDbContext> options) :
        base(options) => _logger = logger;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _logger.LogInformation("OnModelCreating");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}