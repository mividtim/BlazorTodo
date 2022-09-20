using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.Features.Todos.Actions.UpdateTodo;

public class UpdateTodoSuccessAction
{
    public UpdateTodoSuccessAction(TodoDto todo) => Todo = todo;

    public TodoDto Todo { get; }
}