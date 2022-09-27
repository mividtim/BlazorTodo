using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Todos;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTodoClient.Features.Todos.Store.UpdateTodo;

public class UpdateTodoEffect : Effect<UpdateTodoAction>
{
    private readonly ILogger<UpdateTodoEffect> _logger;
    private readonly BlazorTodoApiService _apiService;

    public UpdateTodoEffect(ILogger<UpdateTodoEffect> logger, BlazorTodoApiService apiService) =>
        (_logger, _apiService) = (logger, apiService);

    public override async Task HandleAsync(UpdateTodoAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Updating todo {ActionId}...", action.Id);
            HttpResponseMessage? updateResponse;
            try
            {
                updateResponse = await _apiService.PutAsync($"todos/{action.Id}", action.Todo);
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                return;
            }
            if (!updateResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error updating todo: {updateResponse.ReasonPhrase}");
            }
            _logger.LogInformation("Todo updated successfully!");
            TodoDto? updatedTodo;
            try
            {
                updatedTodo = await _apiService.GetAsync<TodoDto>($"todos/{action.Id}");
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                return;
            }
            dispatcher.Dispatch(new UpdateTodoSuccessAction(updatedTodo!));
        }
        catch (Exception e)
        {
            _logger.LogError("Could not update todo, reason: {EMessage}", e.Message);
            dispatcher.Dispatch(new UpdateTodoFailureAction(e.Message));
        }
    }
}