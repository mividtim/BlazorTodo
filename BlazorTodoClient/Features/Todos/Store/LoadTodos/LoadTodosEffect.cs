using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Todos;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodos;

public class LoadTodosEffect : Effect<LoadTodosAction>
{
    private readonly ILogger<LoadTodosEffect> _logger;
    private readonly BlazorTodoApiClient _apiClient;

    public LoadTodosEffect(ILogger<LoadTodosEffect> logger, BlazorTodoApiClient apiClient) =>
        (_logger, _apiClient) = (logger, apiClient);

    public override async Task HandleAsync(LoadTodosAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Loading todos...");
            IEnumerable<TodoDto>? todosResponse;
            try
            {
                todosResponse = await _apiClient.GetAsync<IEnumerable<TodoDto>>("todos");
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                return;
            }
            _logger.LogInformation("Todos loaded successfully!");
            dispatcher.Dispatch(new LoadTodosSuccessAction((todosResponse ?? new List<TodoDto>(0)).Take(5)));
        }
        catch (Exception e)
        {
            _logger.LogError("Error loading todos, reason: {Message}", e.Message);
            dispatcher.Dispatch(new LoadTodosFailureAction(e.Message));
        } 
    }
}