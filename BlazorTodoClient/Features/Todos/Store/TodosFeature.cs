using System.Diagnostics.CodeAnalysis;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class TodosFeature : Feature<TodosState>
{
    public override string GetName() => "To-Dos";

    protected override TodosState GetInitialState() => new(false, null, null, null);
}