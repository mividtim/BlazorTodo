using System.Net.Http.Json;
using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Todos;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace BlazorTodoClient.Features.Todos.Store.CreateTodo;

public class CreateTodoEffect : Effect<CreateTodoAction>
{
    private readonly ILogger<CreateTodoEffect> _logger;
    private readonly BlazorTodoApiClient _apiClient;
    
    public CreateTodoEffect(ILogger<CreateTodoEffect> logger, BlazorTodoApiClient apiClient) =>
        (_logger, _apiClient) = (logger, apiClient);

    public override async Task HandleAsync(CreateTodoAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Creating todo {ActionTodo}...", action.Todo);
            HttpResponseMessage? createResponse;
            try
            {
                createResponse = await _apiClient.PostAsync("todos", action.Todo);
            }
            catch (AccessTokenNotAvailableException e)
            {
                e.Redirect();
                return;
            }
            if (!createResponse.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error creating todo: {createResponse.ReasonPhrase}");
            }
            _logger.LogInformation("Todo created successfully!");
            var createdTodo = await createResponse.Content.ReadFromJsonAsync<TodoDto>();
            dispatcher.Dispatch(new CreateTodoSuccessAction(createdTodo!));
        }
        catch (Exception e)
        {
            _logger.LogError("Could not create todo, reason: {Message}", e.Message);
            dispatcher.Dispatch(new CreateTodoFailureAction(e.Message));
        }
    }
}