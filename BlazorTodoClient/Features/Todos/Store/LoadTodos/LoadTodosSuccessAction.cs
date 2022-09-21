using BlazorTodoDtos.Todos;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodos;

public class LoadTodosSuccessAction
{
    public LoadTodosSuccessAction(IEnumerable<TodoDto> todos) => Todos = todos;

    public IEnumerable<TodoDto> Todos { get; }
}