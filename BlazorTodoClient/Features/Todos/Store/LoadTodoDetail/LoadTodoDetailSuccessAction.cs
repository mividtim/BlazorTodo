using BlazorTodoDtos.Todos;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;

public class LoadTodoDetailSuccessAction
{
    public LoadTodoDetailSuccessAction(TodoDto todo) => 
        Todo = todo;

    public TodoDto Todo { get; }
}