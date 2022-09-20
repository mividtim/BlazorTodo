using System.Collections.Immutable;
using BlazorTodoService.Models;
using BlazorTodoService.Models.Todos;
using BlazorTodoService.Models.Todos.Dtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly TodoContext _context;

    public TodosController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
    {
        if (_context.Todos is null) return NotFound();
        var todos = await _context.Todos.ToArrayAsync();
        return todos.Select(TodoDto.FromModel).ToImmutableArray();
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(Guid id)
    {
        if (_context.Todos is null) return NotFound();
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null)
            return NotFound();
        return TodoDto.FromModel(todo);
    }

    // PUT: api/todos/5
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(Guid id, TodoDto dto)
    {
        if (id != dto.Id) return BadRequest();
        if (_context.Todos is null) return NotFound();
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        todo.Title = dto.Title;
        todo.Completed = dto.Completed;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id)) return NotFound();
            throw;
        }
        return NoContent();
    }

    // POST: api/todos
    // To protect from over-posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TodoDto>> PostTodo(TodoDto dto)
    {
        if (_context.Todos is null) return Problem("Entity set 'TodoContext.Todos' is null.");
        // TODO: Add UserId
        Todo todo = new() { Title = dto.Title, Completed = dto.Completed };
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, TodoDto.FromModel(todo));
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        if (_context.Todos is null) return NotFound();
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    // PATCH: api/todos/5
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchTodo(Guid id, JsonPatchDocument<Todo> patchDocument)
    {
        if (_context.Todos is null) return NotFound();
        var todo = await _context.Todos.FindAsync(id);
        if (todo is null) return NotFound();
        patchDocument.ApplyTo(todo);
        try
        {
            await _context.SaveChangesAsync();
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
        return (_context.Todos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}