using BlazorTodoClient.Features.Authx.Store.UserLoginOrLogOut;
using BlazorTodoClient.Store.State;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodos;

public static class LoadTodosActionsReducer
{
    [ReducerMethod]
    public static TodosState ReduceLoadTodosAction(TodosState state, LoadTodosAction _) =>
        new(true, null, null, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceLoadTodosSuccessAction(TodosState state, LoadTodosSuccessAction action) =>
        new(false, null, action.Todos, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceLoadTodosFailureAction(TodosState state, LoadTodosFailureAction action) =>
        new(false, action.ErrorMessage, null, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState UserLoggedOut(TodosState state, UserLoginOrLogOutAction _) =>
        new(false, null, null, null);
}