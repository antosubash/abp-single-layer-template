using AbpTemplate.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AbpTemplate.Permissions;

public class AbpTemplatePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var abpTemplateGroup = context.AddGroup(
            AbpTemplatePermissions.GroupName,
            L("Permission:AbpTemplate")
        );
        var tenantPermission = abpTemplateGroup.AddPermission(
            AbpTemplatePermissions.Tenant.Default,
            L("Permission:AbpTemplate.Tenant")
        );
        tenantPermission.AddChild(
            AbpTemplatePermissions.Tenant.AddHost,
            L("Permission:AbpTemplate.Tenant.AddHost")
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpTemplateResource>(name);
    }
}
