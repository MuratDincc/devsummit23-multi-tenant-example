namespace Tenant.Domain.Constants;

public static class CacheKeyConstants
{
    #region Tenant

    public static string Tenants = "DevNot.Example.Tenants";
    public static string TenantById = "DevNot.Example.Tenants.{0}";
    public static string TenantBySlug = "DevNot.Example.TenantsBySlug.{0}";

    #endregion
    
    #region User
    
    public static string UserById = "DevNot.Example.Users.{0}";

    #endregion
}
