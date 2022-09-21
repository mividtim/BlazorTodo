using BlazorTodoClient.Store.State;
using BlazorTodoDtos.Todos;

namespace BlazorTodoClient.Features.Todos.Store;

public class TodosState : BaseState
{
    public TodosState(
        bool isLoading,
        string? currentErrorMessage,
        IEnumerable<TodoDto>? currentTodos,
        TodoDto? currentTodo
    ) : base(isLoading, currentErrorMessage)
    {
        CurrentTodos = currentTodos;
        CurrentTodo = currentTodo;
    }

    public IEnumerable<TodoDto>? CurrentTodos { get; }

    public TodoDto? CurrentTodo { get; }
}