using BlazorTodoClient.Models.Todos.Dtos;
using BlazorTodoClient.Services;
using BlazorTodoClient.Store.Features.Todos.Actions.LoadTodos;
using Fluxor;

namespace BlazorTodoClient.Store.Features.Todos.Effects;

public class LoadTodosEffect : Effect<LoadTodosAction>
{
    private readonly ILogger<LoadTodosEffect> _logger;
    private readonly JsonPlaceholderApiService _apiService;

    public LoadTodosEffect(ILogger<LoadTodosEffect> logger, JsonPlaceholderApiService apiService) =>
        (_logger, _apiService) = (logger, apiService);

    public override async Task HandleAsync(LoadTodosAction action, IDispatcher dispatcher)
    {
        try
        {
            _logger.LogInformation("Loading todos...");
            var todosResponse = await _apiService.GetAsync<IEnumerable<TodoDto>>("todos");
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