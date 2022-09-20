using System.ComponentModel.DataAnnotations;

namespace BlazorTodoClient.Models.Todos.Validation;

public class CreateOrUpdateTodoValidationModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Your todo must have a title")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Status of this todo is required")]
    public bool Completed { get; set; }
}