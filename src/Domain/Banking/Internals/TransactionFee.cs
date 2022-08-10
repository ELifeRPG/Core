namespace ELifeRPG.Domain.Banking.Internals;

public class TransactionFee
{
    public TransactionFee(BankAccountTransaction transaction, BankAccount bankAccount)
    {
        Value = Decimal.Multiply(transaction.Amount, bankAccount.TransactionFeeMultiplier);
    }
    
    public decimal Value { get; }
}
