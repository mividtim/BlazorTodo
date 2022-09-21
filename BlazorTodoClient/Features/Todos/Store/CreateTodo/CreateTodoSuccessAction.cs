using BlazorTodoDtos.Todos;

namespace BlazorTodoClient.Features.Todos.Store.CreateTodo;

public class CreateTodoSuccessAction
{
    public CreateTodoSuccessAction(TodoDto todo) => 
        Todo = todo;

    public TodoDto Todo { get; }
}