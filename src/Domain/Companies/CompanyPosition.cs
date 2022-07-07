using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Companies;

public class CompanyPosition : EntityBase
{
    public Guid Id { get; init; }

    public Company Company { get; init; } = null!;

    public string Name { get; init; } = null!;
    
    public int Ordering { get; init; }
    
    public CompanyPermissions Permissions { get; init; }
}
