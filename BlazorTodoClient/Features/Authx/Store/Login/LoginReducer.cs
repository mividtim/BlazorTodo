using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.Login;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class LoginReducer
{
    [ReducerMethod]
    public static AuthxState ReduceLogin(AuthxState state, LoginAction action) =>
        new(true, null);

    [ReducerMethod]
    public static AuthxState ReduceLoginSuccess(AuthxState state, LoginSuccessAction action) =>
        new(false, null);

    [ReducerMethod]
    public static AuthxState ReduceLoginFailure(AuthxState state, LoginFailureAction action) =>
        new(false, action.ErrorMessage);
}