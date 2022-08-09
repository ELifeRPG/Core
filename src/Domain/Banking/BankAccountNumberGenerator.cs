namespace ELifeRPG.Domain.Banking;

public static class BankAccountNumberGenerator
{
    private static readonly Random _random = new();
    
    public static BankAccountNumber Generate(Bank bank)
    {
        var randomNumber = _random.Next(100000, 999999);
        return new BankAccountNumber("EL", 31, bank.Number, randomNumber);
    }
}
