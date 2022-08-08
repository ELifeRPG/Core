namespace ELifeRPG.Domain.Banking.Accounts;

public class Bank
{
    private readonly List<BankAccount>? _accounts;
    
    public Guid Id { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// The national identification number of the bank.
    /// </summary>
    public int Number { get; init; }

    public IReadOnlyCollection<BankAccount>? Accounts
    {
        get => _accounts;
        init => _accounts = value!.ToList();
    }

    public BankAccount OpenAccount()
    {
        var account = new BankAccount(this);
        _accounts!.Add(account);
        return account;
    }
}
