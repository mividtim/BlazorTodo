namespace BlazorTodoClient.Store.Features.Todos.Actions.DeleteTodo;

public class DeleteTodoSuccessAction
{
    public DeleteTodoSuccessAction(Guid id) => Id = id;

    public Guid Id { get; }
}