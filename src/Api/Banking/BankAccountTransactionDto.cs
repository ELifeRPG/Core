using System.ComponentModel.DataAnnotations;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountTransactionDto
{
    public Guid BankAccountId { get; set; }

    public string? BankAccountNumber { get; set; }

    [Required]
    public Guid TargetBankAccountId { get; set; }

    public string? TargetBankAccountNumber { get; set; }

    [Required]
    public decimal Amount { get; set; }

    public decimal? Fees { get; set; }
}
