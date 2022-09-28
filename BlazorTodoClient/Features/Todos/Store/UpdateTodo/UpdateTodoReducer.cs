using System.Diagnostics.CodeAnalysis;
using BlazorTodoDtos.Todos;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.UpdateTodo;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class UpdateTodoReducer
{
    [ReducerMethod]
    public static TodosState ReduceUpdateTodo(TodosState state, UpdateTodoAction action) =>
        new(true, null, state.CurrentTodos, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceUpdateTodoSuccess(TodosState state, UpdateTodoSuccessAction action)
    {
        // If the current todos list is null, set the state with a new list containing the updated to-do
        if (state.CurrentTodos is null)
        {
            return new TodosState(false, null, new List<TodoDto> { action.Todo }, state.CurrentTodo);
        }
        // Rather than mutating in place, let's construct a new list and add our updated item
        var updatedList = state.CurrentTodos.Where(t => t.Id != action.Todo.Id).ToList();
        // Add the to-do and sort the list
        updatedList.Add(action.Todo);
        updatedList = updatedList.OrderBy(t => t.Id).ToList();
        return new TodosState(false, null, updatedList, null);
    }

    [ReducerMethod]
    public static TodosState ReduceUpdateTodoFailure(TodosState state, UpdateTodoFailureAction action) =>
        new(false, action.ErrorMessage, state.CurrentTodos, state.CurrentTodo);
}