namespace BlazorTodoClient.Features.Todos.Store.DeleteTodo;

public class DeleteTodoAction
{
    public DeleteTodoAction(Guid id) => Id = id;

    public Guid Id { get; }
}