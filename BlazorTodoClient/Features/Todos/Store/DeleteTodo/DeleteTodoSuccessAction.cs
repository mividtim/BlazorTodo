namespace BlazorTodoClient.Features.Todos.Store.DeleteTodo;

public class DeleteTodoSuccessAction
{
    public DeleteTodoSuccessAction(Guid id) => Id = id;

    public Guid Id { get; }
}