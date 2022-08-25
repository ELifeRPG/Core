namespace ELifeRPG.Core.Api.Banking;

public class BankAccountTransactionDto
{
    public Guid BankAccountId { get; set; }

    public string BankAccountNumber { get; set; } = null!;
    
    public Guid? TargetBankAccountId { get; set; }
    
    public string? TargetBankAccountNumber { get; set; }
    
    public decimal Amount { get; set; }
    
    public decimal Fees { get; set; }
}
