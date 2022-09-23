using System.Diagnostics.CodeAnalysis;
using BlazorTodoService.Features.Todos;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace BlazorTodoService;

public partial class BlazorTodoDbContext
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public DbSet<Todo>? Todos { get; set; } = null;
}