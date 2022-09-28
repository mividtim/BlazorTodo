namespace BlazorTodoClient.Features.Navigation.Store;

public interface INavigationService
{
    public void NavigateTo(string route);
}