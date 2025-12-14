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

        var todoPermission = abpTemplateGroup.AddPermission(
            AbpTemplatePermissions.Todo.Default,
            L("Permission:AbpTemplate.Todo")
        );
        todoPermission.AddChild(
            AbpTemplatePermissions.Todo.Create,
            L("Permission:AbpTemplate.Todo.Create")
        );
        todoPermission.AddChild(
            AbpTemplatePermissions.Todo.Update,
            L("Permission:AbpTemplate.Todo.Update")
        );
        todoPermission.AddChild(
            AbpTemplatePermissions.Todo.Delete,
            L("Permission:AbpTemplate.Todo.Delete")
        );
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpTemplateResource>(name);
    }
}
