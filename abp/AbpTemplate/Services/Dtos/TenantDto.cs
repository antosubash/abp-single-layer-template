using Volo.Abp.Application.Dtos;

namespace AbpTemplate.Services.Dtos
{
    public class CustomTenantDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Host { get; set; }
    }
}