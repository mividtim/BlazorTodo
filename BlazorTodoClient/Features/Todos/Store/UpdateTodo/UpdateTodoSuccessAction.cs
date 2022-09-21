using BlazorTodoDtos.Todos;

namespace BlazorTodoClient.Features.Todos.Store.UpdateTodo;

public class UpdateTodoSuccessAction
{
    public UpdateTodoSuccessAction(TodoDto todo) => Todo = todo;

    public TodoDto Todo { get; }
}