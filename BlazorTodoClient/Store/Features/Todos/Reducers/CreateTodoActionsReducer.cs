using BlazorTodoClient.Models.Todos.Dtos;
using BlazorTodoClient.Store.Features.Todos.Actions.CreateTodo;
using BlazorTodoClient.Store.State;
using Fluxor;

namespace BlazorTodoClient.Store.Features.Todos.Reducers;

public static class CreateTodoActionsReducer
{
    [ReducerMethod]
    public static TodosState ReduceCreateTodoAction(TodosState state, CreateTodoAction _) =>
        new(true, null, state.CurrentTodos, state.CurrentTodo);

    [ReducerMethod]
    public static TodosState ReduceCreateTodoSuccessAction(TodosState state, CreateTodoSuccessAction action)
    {
        // Grab a reference to the current todo list, or initialize one if we do not currently have any loaded
        var newTodos = state.CurrentTodos?.ToList() ?? new List<TodoDto>(1);
        // Add the newly created todo to our list and sort by ID
        newTodos.Add(action.Todo);
        newTodos = newTodos.OrderBy(t => t.Id).ToList();
        return new TodosState(false, null, newTodos, state.CurrentTodo);
    }

    [ReducerMethod]
    public static TodosState ReduceCreateTodoFailureAction(TodosState state, CreateTodoFailureAction action) =>
        new(false, action.ErrorMessage, state.CurrentTodos, state.CurrentTodo);
}