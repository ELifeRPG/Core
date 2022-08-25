using ELifeRPG.Domain.Banking;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountDto
{
    public Guid Id { get; init; }
    
    public Guid BankId { get; init; }
    
    public ICollection<BankAccountTransaction>? SentTransactions { get; init; }
    
    public ICollection<BankAccountTransaction>? ReceivedTransactions { get; init; }
}
