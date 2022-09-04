using ELifeRPG.Domain.Banking;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountBookingDto
{
    public BankAccountBookingType Type { get; set; }
    
    public DateTime Date { get; set; }
    
    public string? SourceBankAccountNumber { get; set; }
    
    public string? Purpose { get; set; }
    
    public decimal Amount { get; set; }
}
