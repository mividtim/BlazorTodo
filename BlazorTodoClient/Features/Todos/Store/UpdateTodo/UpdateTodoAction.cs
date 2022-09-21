using BlazorTodoClient.Features.Todos.Models.Dtos;

namespace BlazorTodoClient.Features.Todos.Store.UpdateTodo;

public class UpdateTodoAction
{
    public UpdateTodoAction(Guid id, UpdateTodoDto todo) => 
        (Id, Todo) = (id, todo);

    public Guid Id { get; }

    public UpdateTodoDto Todo { get; }
}