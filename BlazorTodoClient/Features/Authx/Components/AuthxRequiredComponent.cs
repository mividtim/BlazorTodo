using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorTodoClient.Features.Authx.Components;

public class AuthxRequiredComponent : FluxorComponent
{
    #pragma warning disable CS8618
    [Inject] private NavigationManager Navigation { get; set; }
    #pragma warning restore CS8618

    [CascadingParameter] private Task<AuthenticationState>? AuthState { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        if (AuthState is null)
            Navigation.NavigateTo("/login");
        else
        {
            var authState = await AuthState;
            if (authState.User.Identity?.IsAuthenticated != true)
                Navigation.NavigateTo("/login");
        }
    }
}