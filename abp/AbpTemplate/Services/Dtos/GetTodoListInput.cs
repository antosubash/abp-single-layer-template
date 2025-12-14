using AbpTemplate.Entities;
using Volo.Abp.Application.Dtos;

namespace AbpTemplate.Services.Dtos;

public class GetTodoListInput : PagedAndSortedResultRequestDto
{
    public bool? IsCompleted { get; set; }
    public TodoPriority? Priority { get; set; }
}
