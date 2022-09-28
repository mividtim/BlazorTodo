using BlazorTodoClient.Features.Navigation.Store;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorTodoClient.Features.Authx.Components;

public class AuthxAwareComponent : FluxorComponent
{
    [Inject] private INavigationService Navigation { get; set; }

    [CascadingParameter] private Task<AuthenticationState>? AuthState { get; set; }

    protected async Task<bool> EnsureAuthAndRedirect()
    {
        Console.WriteLine("EnsureAuthAndRedirect");
        Console.WriteLine($"AuthState null? {AuthState is null}");
        Console.WriteLine($"IsAuthenticated? {AuthState is not null && (await AuthState).User.Identity?.IsAuthenticated == true}");
        if (AuthState is not null
                && (await AuthState).User.Identity?.IsAuthenticated == true)
            return true;
        Console.WriteLine("Navigating to /login");
        Navigation.NavigateTo("/login");
        return false;
    }
}