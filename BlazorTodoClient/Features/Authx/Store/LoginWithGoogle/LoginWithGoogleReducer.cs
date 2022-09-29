using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.LoginWithGoogle;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class LoginWithGoogleReducer
{
    [ReducerMethod]
    public static AuthxState ReduceLoginWithGoogle(AuthxState state, LoginWithGoogleAction action) =>
        new(true, null);

    [ReducerMethod]
    public static AuthxState ReduceLoginWithGoogleSuccess(AuthxState state, LoginWithGoogleSuccessAction action) =>
        new(false, null);

    [ReducerMethod]
    public static AuthxState ReduceLoginWithGoogleFailure(AuthxState state, LoginWithGoogleFailureAction action) =>
        new(false, action.ErrorMessage);
}