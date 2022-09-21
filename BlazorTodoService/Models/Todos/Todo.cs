using BlazorTodoClient.Features.Todos.Models.Dtos;
using BlazorTodoDtos.Todos;

namespace BlazorTodoService.Models.Todos;

public class Todo
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
    public Guid UserId { get; set; }

    public TodoDto ToDto() =>
        new() { Id = Id, Title = Title, Completed = Completed };

    public CreateTodoDto ToCreateDto() => new(Title, Completed);

    public void MapBackFromCreateDto(CreateTodoDto dto)
    {
        Title = dto.Title;
        Completed = dto.Completed;
    }
}