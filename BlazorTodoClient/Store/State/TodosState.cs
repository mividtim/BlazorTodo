using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.State;

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