namespace BlazorTodoClient.Features.Todos.Models.Dtos;

public class UpdateTodoDto
{
    public UpdateTodoDto(Guid id, string title, bool completed) =>
        (Id, Title, Completed) = (id, title, completed);

    public Guid Id { get; }

    public string Title { get; }

    public bool Completed { get; }
}