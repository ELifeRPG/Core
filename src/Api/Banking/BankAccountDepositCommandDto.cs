using System.ComponentModel.DataAnnotations;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountDepositCommandDto
{
    [Required]
    public decimal Amount { get; set; }
}
