﻿namespace ELifeRPG.Domain.Banking.Internals;

public class TransactionFee
{
    public TransactionFee(BankAccountTransaction transaction, BankAccount bankAccount)
    {
        Value = bankAccount.BankCondition!.TransactionFeeBase + decimal.Multiply(transaction.Amount, bankAccount.BankCondition!.TransactionFeeMultiplier);
    }

    public decimal Value { get; }
}
