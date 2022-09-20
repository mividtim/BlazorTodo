using System.Diagnostics.CodeAnalysis;

namespace BlazorTodoClient.Models.Todos.Dtos;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class TodoDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
}