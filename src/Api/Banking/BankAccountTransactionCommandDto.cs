using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountTransactionCommandDto
{
    [Required]
    [FromBody]
    public Guid TargetBankAccountId { get; set; }

    [Required]
    [FromBody]
    public decimal Amount { get; set; }
}
