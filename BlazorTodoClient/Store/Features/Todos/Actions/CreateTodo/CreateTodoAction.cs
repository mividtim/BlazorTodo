using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.Features.Todos.Actions.CreateTodo;

public class CreateTodoAction
{
    public CreateTodoAction(CreateTodoDto todo) =>
        Todo = todo;

    public CreateTodoDto Todo { get; }
}