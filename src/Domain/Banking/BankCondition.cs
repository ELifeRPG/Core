using ELifeRPG.Domain.Common;

namespace ELifeRPG.Domain.Banking;

public class BankCondition : EntityBase
{
    public static readonly BankCondition Default = new() { TransactionFeeBase = 0.20m, TransactionFeeMultiplier = 0.02m };
    
    public Guid Id { get; init; } = Guid.NewGuid();

    public Bank? Bank { get; init; }
    
    public ICollection<BankAccount>? BankAccounts { get; init; }

    public decimal TransactionFeeBase { get; init; }
    
    public decimal TransactionFeeMultiplier { get; init; }
}
