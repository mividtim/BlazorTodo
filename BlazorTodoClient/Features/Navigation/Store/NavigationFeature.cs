using System.Diagnostics.CodeAnalysis;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace BlazorTodoClient.Features.Navigation.Store;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class NavigationFeature : Feature<NavigationState>
{
    private readonly NavigationManager _navigationManager;

    public NavigationFeature(NavigationManager navigationManager) =>
        _navigationManager = navigationManager;

    public override string GetName() => "Navigation";

    protected override NavigationState GetInitialState() => new(_navigationManager.Uri, null);
}