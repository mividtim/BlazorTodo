using System.Net.Http.Json;
using BlazorTodoClient.Models.Todos.Dtos;
using BlazorTodoClient.Services;
using BlazorTodoClient.Store.Features.Todos.Actions.UpdateTodo;
using Fluxor;

namespace BlazorTodoClient.Store.Features.Todos.Effects;

public class UpdateTodoEffect : Effect<UpdateTodoAction>
{
    private readonly ILogger<UpdateTodoEffect> _logger;
    private readonly JsonPlaceholderApiService _apiService;

    public UpdateTodoEffect(ILogger<UpdateTodoEffect> logger, JsonPlaceholderApiService apiService) =>
        (_logger, _apiService) = (logger, apiService);

    public override async Task HandleAsync(UpdateTodoAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Updating todo {ActionId}...", action.Id);
            var updateResponse = await _apiService.PutAsync($"todos/{action.Id}", action.Todo);
            if (!updateResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error updating todo: {updateResponse.ReasonPhrase}");
            }
            _logger.LogInformation("Todo updated successfully!");
            var updatedTodo = await _apiService.GetAsync<TodoDto>($"todos/{action.Id}");
            dispatcher.Dispatch(new UpdateTodoSuccessAction(updatedTodo!));
        }
        catch (Exception e)
        {
            _logger.LogError("Could not update todo, reason: {EMessage}", e.Message);
            dispatcher.Dispatch(new UpdateTodoFailureAction(e.Message));
        }
    }
}