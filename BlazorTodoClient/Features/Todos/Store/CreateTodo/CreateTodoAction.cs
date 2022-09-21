using BlazorTodoClient.Features.Todos.Models.Dtos;

namespace BlazorTodoClient.Features.Todos.Store.CreateTodo;

public class CreateTodoAction
{
    public CreateTodoAction(CreateTodoDto todo) =>
        Todo = todo;

    public CreateTodoDto Todo { get; }
}