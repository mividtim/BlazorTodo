using BlazorTodoClient.Store.State;
using Fluxor;

namespace BlazorTodoClient.Features.Todos.Store;

public class TodosFeature : Feature<TodosState>
{
    public override string GetName() => "Todos";

    protected override TodosState GetInitialState() => new(false, null, null, null);
}