using BlazorTodoService.Models.Todos;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options)
    {}

    public DbSet<Todo>? Todos { get; set; } = null!;
}