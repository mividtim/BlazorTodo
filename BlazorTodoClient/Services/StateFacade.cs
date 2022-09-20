using BlazorTodoClient.Models.Todos.Dtos;
using BlazorTodoClient.Store.Features.Todos.Actions.CreateTodo;
using BlazorTodoClient.Store.Features.Todos.Actions.DeleteTodo;
using BlazorTodoClient.Store.Features.Todos.Actions.LoadTodoDetail;
using BlazorTodoClient.Store.Features.Todos.Actions.LoadTodos;
using BlazorTodoClient.Store.Features.Todos.Actions.UpdateTodo;
using Fluxor;

namespace BlazorTodoClient.Services;

public class StateFacade
{
    private readonly ILogger<StateFacade> _logger;
    private readonly IDispatcher _dispatcher;

    public StateFacade(ILogger<StateFacade> logger, IDispatcher dispatcher) =>
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
        // Construct our validated todo
        var todoDto = new CreateTodoDto(title, completed);
        _logger.LogInformation("Issuing action to create todo [{Title}]", title);
        _dispatcher.Dispatch(new CreateTodoAction(todoDto));
    }
    
    public void UpdateTodo(Guid id, string title, bool completed)
    {
        // Construct our validated todo
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