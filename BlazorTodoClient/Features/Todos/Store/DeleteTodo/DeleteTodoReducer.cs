using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.DeleteTodo;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class DeleteTodoReducer
{
    [ReducerMethod]
    public static TodosState ReduceDeleteTodo(TodosState state, DeleteTodoAction action) =>
        new(true, null, state.CurrentTodos, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceDeleteTodoSuccess(TodosState state, DeleteTodoSuccessAction action)
    {
        // Return the default state if no list of todos is found
        if (state.CurrentTodos is null)
        {
            return new TodosState(false, null, null, state.CurrentTodo);
        }
        // Create a new list with all to-do items excluding the to-do with the deleted ID
        var updatedTodos = state.CurrentTodos.Where(t => t.Id != action.Id);
        return new TodosState(false, null, updatedTodos, state.CurrentTodo);
    }

    [ReducerMethod]
    public static TodosState ReduceDeleteTodoFailure(TodosState state, DeleteTodoFailureAction action) =>
        new(false, action.ErrorMessage, state.CurrentTodos, state.CurrentTodo);
}