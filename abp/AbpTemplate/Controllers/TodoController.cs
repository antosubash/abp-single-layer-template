using System.Linq.Dynamic.Core;
using AbpTemplate.Entities;
using AbpTemplate.ObjectMapping;
using AbpTemplate.Permissions;
using AbpTemplate.Services.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Domain.Repositories;

namespace AbpTemplate.Controllers;

[Route("api/todos")]
[Authorize(AbpTemplatePermissions.Todo.Default)]
public class TodoController : AbpController
{
    private readonly IRepository<Todo, Guid> _todoRepository;
    private readonly TodoToTodoDtoMapper _todoMapper;
    private readonly CreateTodoDtoToTodoMapper _createMapper;
    private readonly UpdateTodoDtoToTodoMapper _updateMapper;

    public TodoController(
        IRepository<Todo, Guid> todoRepository,
        TodoToTodoDtoMapper todoMapper,
        CreateTodoDtoToTodoMapper createMapper,
        UpdateTodoDtoToTodoMapper updateMapper
    )
    {
        _todoRepository = todoRepository;
        _todoMapper = todoMapper;
        _createMapper = createMapper;
        _updateMapper = updateMapper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultDto<TodoDto>>> GetListAsync(
        [FromQuery] GetTodoListInput input
    )
    {
        var query = await _todoRepository.GetQueryableAsync();

        if (input.IsCompleted.HasValue)
        {
            query = query.Where(x => x.IsCompleted == input.IsCompleted.Value);
        }

        if (input.Priority.HasValue)
        {
            query = query.Where(x => x.Priority == input.Priority.Value);
        }

        var totalCount = await query.CountAsync();

        var sorting = input.Sorting ?? "CreationTime DESC";
        query = query.OrderBy(sorting);

        var items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

        var dtos = items.Select(x => _todoMapper.Map(x)).ToList();

        return Ok(new PagedResultDto<TodoDto> { TotalCount = totalCount, Items = dtos });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoDto>> GetAsync(Guid id)
    {
        var todo = await _todoRepository.GetAsync(id);
        return Ok(_todoMapper.Map(todo));
    }

    [HttpPost]
    [Authorize(AbpTemplatePermissions.Todo.Create)]
    public async Task<ActionResult<TodoDto>> CreateAsync(CreateTodoDto input)
    {
        var todo = _createMapper.Map(input);
        todo.IsCompleted = false;
        await _todoRepository.InsertAsync(todo);
        return Ok(_todoMapper.Map(todo));
    }

    [HttpPut("{id}")]
    [Authorize(AbpTemplatePermissions.Todo.Update)]
    public async Task<ActionResult<TodoDto>> UpdateAsync(Guid id, UpdateTodoDto input)
    {
        var todo = await _todoRepository.GetAsync(id);
        _updateMapper.Map(input, todo);
        await _todoRepository.UpdateAsync(todo);
        return Ok(_todoMapper.Map(todo));
    }

    [HttpDelete("{id}")]
    [Authorize(AbpTemplatePermissions.Todo.Delete)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _todoRepository.DeleteAsync(id);
        return Ok();
    }

    [HttpPatch("{id}/toggle-complete")]
    [Authorize(AbpTemplatePermissions.Todo.Update)]
    public async Task<ActionResult<TodoDto>> ToggleCompleteAsync(Guid id)
    {
        var todo = await _todoRepository.GetAsync(id);
        todo.IsCompleted = !todo.IsCompleted;
        await _todoRepository.UpdateAsync(todo);
        return Ok(_todoMapper.Map(todo));
    }
}
