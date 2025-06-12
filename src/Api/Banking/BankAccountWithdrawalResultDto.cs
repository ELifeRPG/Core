namespace ELifeRPG.Core.Api.Banking;

public class BankAccountWithdrawalResultDto
{
    public Guid BankAccountId { get; set; }

    public string? BankAccountNumber { get; set; }

    public decimal Amount { get; set; }

    public decimal? Fees { get; set; }
}
