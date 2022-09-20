namespace BlazorTodoService.Models.Todos.Dtos;

public class TodoDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }

    public static TodoDto FromModel(Todo model) => new() { Id = model.Id, Title = model.Title, Completed = model.Completed };
}