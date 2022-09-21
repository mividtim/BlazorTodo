using BlazorTodoClient.Features.Todos.Models.Dtos;
using BlazorTodoClient.Features.Todos.Store.CreateTodo;
using BlazorTodoClient.Features.Todos.Store.DeleteTodo;
using BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;
using BlazorTodoClient.Features.Todos.Store.LoadTodos;
using BlazorTodoClient.Features.Todos.Store.UpdateTodo;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store;

public class TodosStateFacade
{
    private readonly ILogger<TodosStateFacade> _logger;
    private readonly IDispatcher _dispatcher;

    public TodosStateFacade(ILogger<TodosStateFacade> logger, IDispatcher dispatcher) =>
        (_logger, _dispatcher) = (logger, dispatcher);

    public void LoadTodos()
    {
        _logger.LogInformation("Issuing action to load todos...");
        _dispatcher.Dispatch(new LoadTodosAction());
    }
    
    public void LoadTodoById(Guid id)
    {
        _logger.LogInformation("Issuing action to load todo {Id}...", id);
        _dispatcher.Dispatch(new LoadTodoDetailAction(id));
    }

    public void CreateTodo(string title, bool completed)
    {
        // Construct our validated to-do
        var todoDto = new CreateTodoDto(title, completed);
        _logger.LogInformation("Issuing action to create todo [{Title}]", title);
        _dispatcher.Dispatch(new CreateTodoAction(todoDto));
    }
    
    public void UpdateTodo(Guid id, string title, bool completed)
    {
        // Construct our validated to-do
        var todoDto = new UpdateTodoDto(id, title, completed);
        _logger.LogInformation("Issuing action to update todo {Id}", id);
        _dispatcher.Dispatch(new UpdateTodoAction(id, todoDto));
    }

    public void DeleteTodo(Guid id)
    {
        _logger.LogInformation("Issuing action to delete todo {Id}", id);
        _dispatcher.Dispatch(new DeleteTodoAction(id));
    }
}