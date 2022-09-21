namespace BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;

public class LoadTodoDetailAction
{
    public LoadTodoDetailAction(Guid id) => Id = id;

    public Guid Id { get; set; }
}