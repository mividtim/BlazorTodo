using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorTodoClient.Features.Todos.Models.Dtos;
using BlazorTodoDtos.Todos;
using BlazorTodoService.Features.Authx;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorTodoService.Features.Todos;

[Route("api/todos")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ILogger<TodosController> _logger;
    private readonly BlazorTodoDbContext _dbContext;
    private readonly UserManager<AuthxUser> _userManager;

    public TodosController(
        ILogger<TodosController> logger,
        BlazorTodoDbContext dbContext,
        UserManager<AuthxUser> userManager
    ) =>
        (_logger, _dbContext, _userManager) = (logger, dbContext, userManager);

    // GET: api/todos
    [HttpGet]
    public ActionResult<IEnumerable<TodoDto>> GetTodos()
    {
        if (_dbContext.Todos is null) return NotFound();
        var userId = GetUserId();
        if (userId is null) return Unauthorized();
        return _dbContext.Todos
            .Where(todo => todo.UserId == userId)
            .Select(todo => todo.ToDto())
            .ToImmutableArray();
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetTodo(Guid id)
    {
        var todo = await GetTodoForCurrentUser(id);
        if (todo is null) return NotFound();
        return todo.ToDto();
    }

    // PUT: api/todos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodo(Guid id, UpdateTodoDto dto)
    {
        if (id != dto.Id) return UnprocessableEntity();
        var todo = await GetTodoForCurrentUser(id);
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
        if (_dbContext.Todos is null) return NotFound();
        try
        {
            var userId = GetUserId() ?? throw new UnauthorizedAccessException();
            Todo todo = new() { Title = dto.Title, Completed = dto.Completed, UserId = userId };
            _dbContext.Todos.Add(todo);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo.ToDto());
        }
        catch (Exception e)
        {
            if (e is UnauthorizedAccessException) return Unauthorized();
            throw;
        }
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        if (_dbContext.Todos is null) return NotFound();
        var todo = await GetTodoForCurrentUser(id);
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
        return (_dbContext.Todos?.Any(todo => todo.Id == id)).GetValueOrDefault();
    }

    private Guid? GetUserId()
    {
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            _logger.LogWarning("UserId not found in claims");
            return null;
        }

        _logger.LogInformation("UserId is {UserId}", userId);
        return userId;
    }

    private async Task<Todo?> GetTodoForCurrentUser(Guid id)
    {
        if (_dbContext.Todos is null) return null;
        var userId = GetUserId();
        if (userId is null) return null;
        return await _dbContext.Todos.FirstOrDefaultAsync(todo => todo.UserId == userId && todo.Id == id);
    }
}