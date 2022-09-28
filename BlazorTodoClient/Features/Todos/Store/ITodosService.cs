namespace BlazorTodoClient.Features.Todos.Store;

public interface ITodosService
{
    public void LoadTodos();
    public void LoadTodoById(Guid id);
    public void CreateTodo(string title, bool completed);
    public void UpdateTodo(Guid id, string title, bool completed);
    public void DeleteTodo(Guid id);
}