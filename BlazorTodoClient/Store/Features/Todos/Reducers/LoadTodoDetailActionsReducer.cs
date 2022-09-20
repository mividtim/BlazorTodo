using BlazorTodoClient.Store.Features.Todos.Actions.LoadTodoDetail;
using BlazorTodoClient.Store.State;
using Fluxor;

namespace BlazorTodoClient.Store.Features.Todos.Reducers;

public static class LoadTodoDetailActionsReducer
{
    [ReducerMethod]
    public static TodosState ReduceLoadTodoDetailAction(TodosState state, LoadTodoDetailAction _) =>
        new(true, null, state.CurrentTodos, null);

    [ReducerMethod]
    public static TodosState ReduceLoadTodoDetailSuccessAction(TodosState state, LoadTodoDetailSuccessAction action) =>
        new(false, null, state.CurrentTodos, action.Todo);

    [ReducerMethod]
    public static TodosState ReduceLoadTodoDetailFailureAction(TodosState state, LoadTodoDetailFailureAction action) =>
        new(false, action.ErrorMessage, state.CurrentTodos, null);
}