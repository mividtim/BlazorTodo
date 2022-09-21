using System.Net.Http.Json;
using BlazorTodoClient.ServiceClients;
using BlazorTodoDtos.Todos;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store.CreateTodo;

public class CreateTodoEffect : Effect<CreateTodoAction>
{
    private readonly ILogger<CreateTodoEffect> _logger;
    private readonly BlazorTodoApiService _apiService;
    
    public CreateTodoEffect(ILogger<CreateTodoEffect> logger, BlazorTodoApiService apiService) =>
        (_logger, _apiService) = (logger, apiService);

    public override async Task HandleAsync(CreateTodoAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Creating todo {ActionTodo}...", action.Todo);
            var createResponse = await _apiService.PostAsync("todos", action.Todo);
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