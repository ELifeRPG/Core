using System.ComponentModel.DataAnnotations;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountDepositResultDto
{
    [Required]
    public decimal Amount { get; set; }

    public decimal? Fees { get; set; }
}
