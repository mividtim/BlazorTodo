namespace BlazorTodoClient.Store.Features.Todos.Actions.DeleteTodo;

public class DeleteTodoAction
{
    public DeleteTodoAction(Guid id) => Id = id;

    public Guid Id { get; }
}