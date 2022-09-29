using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Todos;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;

public class LoadTodoDetailEffect : Effect<LoadTodoDetailAction>
{
    private readonly ILogger<LoadTodoDetailEffect> _logger;
    private readonly BlazorTodoApiClient _apiClient;

    public LoadTodoDetailEffect(ILogger<LoadTodoDetailEffect> logger, BlazorTodoApiClient apiClient) =>
        (_logger, _apiClient) = (logger, apiClient);

    public override async Task HandleAsync(LoadTodoDetailAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Loading todo {ActionId}...", action.Id);
            TodoDto? todoResponse;
            try
            {
                todoResponse = await _apiClient.GetAsync<TodoDto>($"todos/{action.Id}");
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                return;
            }
            _logger.LogInformation("Todo {ActionId} loaded successfully!", action.Id);
            dispatcher.Dispatch(new LoadTodoDetailSuccessAction(todoResponse!));
        }
        catch (Exception e)
        {
            _logger.LogError("Error loading todo {ActionId}, reason: {Message}", action.Id, e.Message);
            dispatcher.Dispatch(new LoadTodoDetailFailureAction(e.Message));
        }
    }
}