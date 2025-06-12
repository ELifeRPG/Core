namespace ELifeRPG.Domain.Companies;

public static class CompanyPermissionsExtensions
{
    public static bool Contains(this CompanyPermissions self, CompanyPermissions flag)
    {
        return (self & flag) == flag;
    }
}
