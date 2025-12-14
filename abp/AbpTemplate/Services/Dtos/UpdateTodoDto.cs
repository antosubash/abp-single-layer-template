using System.ComponentModel.DataAnnotations;
using AbpTemplate.Entities;

namespace AbpTemplate.Services.Dtos;

public class UpdateTodoDto
{
    [Required]
    [MaxLength(256)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; }

    public DateTime? DueDate { get; set; }

    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
}
