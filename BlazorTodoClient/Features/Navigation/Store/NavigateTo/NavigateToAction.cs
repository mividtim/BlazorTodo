namespace BlazorTodoClient.Features.Navigation.Store.NavigateTo;

public class NavigateToAction
{
    public NavigateToAction(string? route) => Route = route ?? "/";

    public string Route { get; }
}