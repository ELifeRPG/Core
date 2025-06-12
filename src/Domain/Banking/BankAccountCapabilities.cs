namespace ELifeRPG.Domain.Banking;

[Flags]
public enum BankAccountCapabilities
{
    None = 1,
    ViewTransactions = None << 1,
    CommitTransactions = ViewTransactions << 1,
}
