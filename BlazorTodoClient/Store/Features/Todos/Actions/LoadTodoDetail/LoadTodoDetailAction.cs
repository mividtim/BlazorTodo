namespace BlazorTodoClient.Store.Features.Todos.Actions.LoadTodoDetail;

public class LoadTodoDetailAction
{
    public LoadTodoDetailAction(Guid id) => Id = id;

    public Guid Id { get; set; }
}