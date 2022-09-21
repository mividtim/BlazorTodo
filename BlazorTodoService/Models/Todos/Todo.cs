using BlazorTodoDtos.Todos;

namespace BlazorTodoService.Models.Todos;

public class Todo
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
    public Guid UserId { get; set; }

    public static TodoDto ToDto(Todo model) =>
        new() { Id = model.Id, Title = model.Title, Completed = model.Completed };
}