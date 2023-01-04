using Volo.Abp.Application.Dtos;

namespace AbpTemplate.Services.Dtos
{
    public class ScopeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Resources { get; set; }
        public string Properties { get; set; }
    }
}