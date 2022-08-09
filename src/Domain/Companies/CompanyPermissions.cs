namespace ELifeRPG.Domain.Companies;

[Flags]
public enum CompanyPermissions
{
    /// <summary>
    /// Default - nothing.
    /// </summary>
    None = 1,
    
    /// <summary>
    /// Manage company details.
    /// </summary>
    ManageCompany = None << 1,
    
    /// <summary>
    /// Manage company members.
    /// </summary>
    ManageMembers = ManageCompany << 1,
    
    /// <summary>
    /// Manage wages for the company. Can configure the wages for company members.
    /// </summary>
    ManageWages = ManageMembers << 1,
    
    /// <summary>
    /// Manage finances for the company. Can send money to other individuals or companies.
    /// </summary>
    ManageFinances = ManageWages << 1,
}

public static class CompanyPermissionsExtensions
{
    public static bool Contains(this CompanyPermissions self, CompanyPermissions flag)
    {
        return (self & flag) == flag;
    }
}
