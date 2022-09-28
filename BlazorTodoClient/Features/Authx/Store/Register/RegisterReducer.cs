using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store.Register;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class RegisterReducer
{
    [ReducerMethod]
    public static AuthxState ReduceRegister(AuthxState state, RegisterAction action) =>
        new(true, null);

    [ReducerMethod]
    public static AuthxState ReduceRegisterSuccess(AuthxState state, RegisterSuccessAction action) =>
        new(false, null);

    [ReducerMethod]
    public static AuthxState ReduceRegisterFailure(AuthxState state, RegisterFailureAction action) =>
        new(false, action.ErrorMessage);
}