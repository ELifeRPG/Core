namespace ELifeRPG.Domain.Banking;

public static class BankAccountCapabilitiesExtensions
{
    public static bool Contains(this BankAccountCapabilities self, BankAccountCapabilities flag)
    {
        return (self & flag) == flag;
    }
}
