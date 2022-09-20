using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.Features.Todos.Actions.CreateTodo;

public class CreateTodoSuccessAction
{
    public CreateTodoSuccessAction(TodoDto todo) => 
        Todo = todo;

    public TodoDto Todo { get; }
}