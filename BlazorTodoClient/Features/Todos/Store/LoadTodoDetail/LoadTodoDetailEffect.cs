using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Todos;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.LoadTodoDetail;

public class LoadTodoDetailEffect : Effect<LoadTodoDetailAction>
{
    private readonly ILogger<LoadTodoDetailEffect> _logger;
    private readonly BlazorTodoApiService _apiService;

    public LoadTodoDetailEffect(ILogger<LoadTodoDetailEffect> logger, BlazorTodoApiService apiService) =>
        (_logger, _apiService) = (logger, apiService);

    public override async Task HandleAsync(LoadTodoDetailAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Loading todo {ActionId}...", action.Id);
            var todoResponse = await _apiService.GetAsync<TodoDto>($"todos/{action.Id}");
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