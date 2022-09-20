using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.Features.Todos.Actions.LoadTodoDetail;

public class LoadTodoDetailSuccessAction
{
    public LoadTodoDetailSuccessAction(TodoDto todo) => 
        Todo = todo;

    public TodoDto Todo { get; }
}