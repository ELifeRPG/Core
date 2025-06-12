namespace ELifeRPG.Core.Api.Banking;

public enum BankAccountBookingTypeEnumDto
{
    Incoming = 1,
    Outgoing = 2,
}

public class BankAccountBookingDto
{
    public BankAccountBookingTypeEnumDto Type { get; set; }

    public DateTime Date { get; set; }

    public string? SourceBankAccountNumber { get; set; }

    public string? Purpose { get; set; }

    public decimal Amount { get; set; }
}
