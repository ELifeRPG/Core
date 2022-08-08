namespace ELifeRPG.Domain.Banking.Accounts;

public class BankAccount
{
    public BankAccount(Bank bank)
    {
        Bank = bank;
        Number = BankAccountNumber.Generate(Bank).Value;
    }
    
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// The international EBAN of the bank account.
    /// </summary>
    public string Number { get; init; } = null!;
    
    public Bank Bank { get; init; }
}
