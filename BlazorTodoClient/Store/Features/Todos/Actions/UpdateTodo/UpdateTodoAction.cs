using BlazorTodoClient.Models.Todos.Dtos;

namespace BlazorTodoClient.Store.Features.Todos.Actions.UpdateTodo;

public class UpdateTodoAction
{
    public UpdateTodoAction(Guid id, UpdateTodoDto todo) => 
        (Id, Todo) = (id, todo);

    public Guid Id { get; }

    public UpdateTodoDto Todo { get; }
}