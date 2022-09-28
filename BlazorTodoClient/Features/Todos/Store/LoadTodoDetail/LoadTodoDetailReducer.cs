using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class LoadTodoDetailReducer
{
    [ReducerMethod]
    public static TodosState ReduceLoadTodoDetail(TodosState state, LoadTodoDetailAction action) =>
        new(true, null, state.CurrentTodos, null);

    [ReducerMethod]
    public static TodosState ReduceLoadTodoDetailSuccess(TodosState state, LoadTodoDetailSuccessAction action) =>
        new(false, null, state.CurrentTodos, action.Todo);

    [ReducerMethod]
    public static TodosState ReduceLoadTodoDetailFailure(TodosState state, LoadTodoDetailFailureAction action) =>
        new(false, action.ErrorMessage, state.CurrentTodos, null);
}