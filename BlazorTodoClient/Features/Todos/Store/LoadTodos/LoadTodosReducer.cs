using System.Diagnostics.CodeAnalysis;
using BlazorTodoClient.Features.Authx.Store.Logout;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodos;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class LoadTodosReducer
{
    [ReducerMethod]
    public static TodosState ReduceLoadTodos(TodosState state, LoadTodosAction action) =>
        new(true, null, null, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceLoadTodosSuccess(TodosState state, LoadTodosSuccessAction action) =>
        new(false, null, action.Todos, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceLoadTodosFailure(TodosState state, LoadTodosFailureAction action) =>
        new(false, action.ErrorMessage, null, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState UserLoggedOut(TodosState state, LogoutSuccessAction action) =>
        new(false, null, null, null);
}