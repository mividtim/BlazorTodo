using System.Collections.Immutable;
using BlazorTodoClient.Features.Todos.Models.Dtos;
using BlazorTodoDtos.Todos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService.Features.Todos;

[Route("api/todos")]
[ApiController]
[Authorize(Roles="Visitor")]
public class TodosController : ControllerBase
{
    private readonly TodosDbContext _dbContext;

    public TodosController(TodosDbContext dbContext) => _dbContext = dbContext;

    // GET: api/todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
    {
        if (_dbContext.Todos is null) return NotFound();
        var todos = await _dbContext.Todos.ToArrayAsync();
        return todos.Select(todo => todo.ToDto()).ToImmutableArray();
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(Guid id)
    {
        if (_dbContext.Todos is null) return NotFound();
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        return todo.ToDto();
    }

    // PUT: api/todos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(Guid id, UpdateTodoDto dto)
    {
        if (id != dto.Id) return UnprocessableEntity();
        if (_dbContext.Todos is null) return NotFound();
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        todo.Title = dto.Title;
        todo.Completed = dto.Completed;
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // POST: api/todos
    [HttpPost]
    public async Task<ActionResult<TodoDto>> PostTodo(CreateTodoDto dto)
    {
        if (_dbContext.Todos is null) return Problem("Entity set 'TodoContext.Todos' is null.");
        // TODO: Add UserId
        Todo todo = new() { Title = dto.Title, Completed = dto.Completed };
        _dbContext.Todos.Add(todo);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id });
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        if (_dbContext.Todos is null) return NotFound();
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        _dbContext.Todos.Remove(todo);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
    
    // PATCH: api/todos/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchTodo(Guid id, JsonPatchDocument<CreateTodoDto> patchDocument)
    {
        if (_dbContext.Todos is null) return NotFound();
        var todo = await _dbContext.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        // Map the updatable values to a create entity, since we don't want the user to be able to update the ID
        var createDto = todo.ToCreateDto();
        patchDocument.ApplyTo(createDto);
        // Validate the changes, then map the values back to the entity and save the changes
        if (!TryValidateModel(createDto)) return UnprocessableEntity();
        todo.MapBackFromCreateDto(createDto);
        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    private bool TodoExists(Guid id)
    {
        return (_dbContext.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}