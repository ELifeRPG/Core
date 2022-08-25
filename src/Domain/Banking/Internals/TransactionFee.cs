namespace ELifeRPG.Domain.Banking.Internals;

public struct TransactionFee
{
    public TransactionFee(BankAccountTransaction transaction, BankAccount bankAccount)
    {
        Value = bankAccount.BankCondition!.TransactionFeeBase + Decimal.Multiply(transaction.Amount, bankAccount.BankCondition!.TransactionFeeMultiplier);
    }
    
    public decimal Value { get; }
}
