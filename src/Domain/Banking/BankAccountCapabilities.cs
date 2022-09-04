namespace ELifeRPG.Domain.Banking;

[Flags]
public enum BankAccountCapabilities
{
    None = 1,
    ViewTransactions = None << 1,
    CommitTransactions = ViewTransactions << 1,
}

public static class BankAccountCapabilitiesExtensions
{
    public static bool Contains(this BankAccountCapabilities self, BankAccountCapabilities flag)
    {
        return (self & flag) == flag;
    }
}
