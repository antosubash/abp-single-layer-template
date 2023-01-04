using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace AbpTemplate.Data;

public class AbpTemplateEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public AbpTemplateEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AbpTemplateDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AbpTemplateDbContext>()
            .Database
            .MigrateAsync();
    }
}
