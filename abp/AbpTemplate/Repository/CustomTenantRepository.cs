using AbpTemplate.Data;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement;

namespace AbpTemplate.Repository
{
    public class CustomTenantRepository : EfCoreRepository<AbpTemplateDbContext, Tenant, Guid>
    {
        public CustomTenantRepository(IDbContextProvider<AbpTemplateDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Tenant> GetTenantByHost(string host, CancellationToken cancellationToken = default)
        {
            var context = await GetDbContextAsync();
            var tenant = context.Tenants.Where(u => EF.Property<string>(u, "Host") == host);
            return await tenant.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}