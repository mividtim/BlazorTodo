using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Authx.Store;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AuthxFeature : Feature<AuthxState>
{
    public override string GetName() => "Authx";

    protected override AuthxState GetInitialState() => new AuthxState(false, null);
}