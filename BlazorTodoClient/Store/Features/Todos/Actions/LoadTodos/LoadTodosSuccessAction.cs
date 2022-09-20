using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.Features.Todos.Actions.LoadTodos;

public class LoadTodosSuccessAction
{
    public LoadTodosSuccessAction(IEnumerable<TodoDto> todos) => Todos = todos;

    public IEnumerable<TodoDto> Todos { get; }
}