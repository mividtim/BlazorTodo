namespace BlazorTodoClient.Features.Todos.Models.Dtos;

public class CreateTodoDto
{
    public CreateTodoDto(string title, bool completed) =>
        (Title, Completed) = (title, completed);

    public string Title { get; }

    public bool Completed { get; }
}