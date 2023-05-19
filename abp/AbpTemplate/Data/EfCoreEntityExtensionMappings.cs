using AbpTemplate.Extensions;
using AbpTemplate.Utils;
using Volo.Abp.ObjectExtending;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;

namespace AbpTemplate.Data;

public static class EfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();
    public static void Configure()
    {
        ModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .MapEfCoreProperty<Tenant, string>(Constant.Host);
        });
    }
}