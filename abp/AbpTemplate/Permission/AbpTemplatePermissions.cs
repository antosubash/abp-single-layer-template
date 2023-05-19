namespace AbpTemplate.Permissions;

public static class AbpTemplatePermissions
{
    public const string GroupName = "AbpTemplate";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Tenant
    {
        public const string Default = GroupName + ".Tenant";
        public const string AddHost = Default + ".AddHost";
    }
}