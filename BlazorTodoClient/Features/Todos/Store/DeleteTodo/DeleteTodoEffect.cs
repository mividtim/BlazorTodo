using BlazorTodoClient.ServiceClients;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.DeleteTodo;

public class DeleteTodoEffect : Effect<DeleteTodoAction>
{
    private readonly ILogger<DeleteTodoEffect> _logger;
    private readonly BlazorTodoApiService _apiService;

    public DeleteTodoEffect(ILogger<DeleteTodoEffect> logger, BlazorTodoApiService apiService) =>
        (_logger, _apiService) = (logger, apiService);

    public override async Task HandleAsync(DeleteTodoAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Deleting todo {ActionId}...", action.Id);
            var deleteResponse = await _apiService.DeleteAsync($"todos/{action.Id}");
            if (!deleteResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error deleting todo: {deleteResponse.ReasonPhrase}");
            }
            _logger.LogInformation($"Todo deleted successfully!");
            dispatcher.Dispatch(new DeleteTodoSuccessAction(action.Id));
        }
        catch (Exception e)
        {
            _logger.LogError("Could not create todo, reason: {Message}", e.Message);
            dispatcher.Dispatch(new DeleteTodoFailureAction(e.Message));
        }
    }
}