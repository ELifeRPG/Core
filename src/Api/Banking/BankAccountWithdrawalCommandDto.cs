using System.ComponentModel.DataAnnotations;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountWithdrawalCommandDto
{
    [Required]
    public decimal Amount { get; set; }
}
