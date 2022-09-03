using ELifeRPG.Domain.Banking;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountBookingDto
{
    public BankAccountBookingType Type { get; set; }
    
    public BankAccountDto? Source { get; init; }
    
    public string? Purpose { get; set; }
    
    public decimal Amount { get; set; }
}
