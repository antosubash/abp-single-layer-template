using Volo.Abp.Application.Dtos;

namespace AbpTemplate.Services.Dtos
{
    public class ClientDto : EntityDto
    {
        public string ClientId { get; set; }
        public string DisplayName { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        public string RedirectUris { get; set; }
        public string Permissions { get; set; }
        public string Type { get; set; }
    }
}