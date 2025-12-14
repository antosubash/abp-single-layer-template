using AbpTemplate.Entities;
using AbpTemplate.Services.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace AbpTemplate.ObjectMapping;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Source)]
[MapExtraProperties]
public partial class TodoToTodoDtoMapper : TwoWayMapperBase<Todo, TodoDto>
{
    public override partial TodoDto Map(Todo source);

    public override partial void Map(Todo source, TodoDto destination);

    public override partial Todo ReverseMap(TodoDto destination);

    public override partial void ReverseMap(TodoDto destination, Todo source);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Source)]
public partial class CreateTodoDtoToTodoMapper : MapperBase<CreateTodoDto, Todo>
{
    public override partial Todo Map(CreateTodoDto source);

    public override partial void Map(CreateTodoDto source, Todo destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Source)]
public partial class UpdateTodoDtoToTodoMapper : MapperBase<UpdateTodoDto, Todo>
{
    public override partial Todo Map(UpdateTodoDto source);

    public override partial void Map(UpdateTodoDto source, Todo destination);
}
