using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService.Features.Todos;

public class TodosDbContext : DbContext
{
    private readonly ILogger<TodosDbContext> _logger;

    public TodosDbContext(ILogger<TodosDbContext> logger, DbContextOptions<TodosDbContext> options) :
        base(options) => _logger = logger;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public DbSet<Todo>? Todos { get; set; } = null;
}