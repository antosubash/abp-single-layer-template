using Riok.Mapperly.Abstractions;

namespace AbpTemplate.ObjectMapping;

// Example mapper structure for future use
// Uncomment and modify as needed when adding mappings

// For one-way mapping (Entity -> DTO):
// [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
// public partial class EntityToEntityDtoMapper : MapperBase<Entity, EntityDto>
// {
//     public override partial EntityDto Map(Entity source);
//     public override partial void Map(Entity source, EntityDto destination);
// }

// For two-way mapping (Entity <-> DTO):
// [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
// public partial class EntityToEntityDtoMapper : TwoWayMapperBase<Entity, EntityDto>
// {
//     public override partial EntityDto Map(Entity source);
//     public override partial void Map(Entity source, EntityDto destination);
//     public override partial Entity ReverseMap(EntityDto destination);
//     public override partial void ReverseMap(EntityDto destination, Entity source);
// }

// For entities with extra properties support:
// [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
// [MapExtraProperties]
// public partial class EntityToEntityDtoMapper : MapperBase<Entity, EntityDto>
// {
//     public override partial EntityDto Map(Entity source);
//     public override partial void Map(Entity source, EntityDto destination);
// }
