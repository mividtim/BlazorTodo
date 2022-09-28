using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Features.Navigation.Store;

public class NavigationState
{
    public NavigationState(string currentRoute, string? previousRoute) =>
        (CurrentRoute, PreviousRoute) = (currentRoute, previousRoute);

    public string CurrentRoute { get; }
    public string? PreviousRoute { get; }
}