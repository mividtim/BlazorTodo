using BlazorTodoClient.Store;

namespace BlazorTodoClient.Features.Authx.Store;

public class AuthxState : BaseState
{
    public AuthxState(bool isLoading, string? currentErrorMessage) : base(isLoading, currentErrorMessage)
    {
    }
}