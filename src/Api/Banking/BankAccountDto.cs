using ELifeRPG.Domain.Banking;

namespace ELifeRPG.Core.Api.Banking;

public class BankAccountDto
{
    public Guid Id { get; set; }

    public string Number { get; set; } = null!;
    
    public decimal Balance { get; set; }
    
    public List<BankAccountBooking>? Bookings { get; set; }
}
