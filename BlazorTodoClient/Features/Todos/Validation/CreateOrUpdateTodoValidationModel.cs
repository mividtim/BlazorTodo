using System.ComponentModel.DataAnnotations;

namespace BlazorTodoClient.Features.Todos.Models.Validation;

public class CreateOrUpdateTodoValidationModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Your to-do must have a title")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Status of this to-do is required")]
    public bool Completed { get; set; }
}