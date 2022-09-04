using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Characters;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Banking;

public class BankAccountBookingTests
{
    [Fact]
    public void Amount_SubtractsFeesFromAmount_WhenOfTransactionTypeDeposit()
    {
        var character = new Character();
        var bankAccount = new BankAccount
        {
            Type = BankAccountType.Personal,
            OwningCharacter = character,
            BankCondition = new BankCondition
            {
                TransactionFeeBase = 0.20m,
                TransactionFeeMultiplier = 0.05m,
            }
        };
        
        var transaction = new BankAccountTransaction(bankAccount, BankAccountTransactionType.CashDeposit, 100);
        var booking = new BankAccountBooking(bankAccount, transaction);
        
        Assert.Equal(94.8m, booking.Amount);
    }
    
    [Fact]
    public void Amount_AddsFeesToAmount_WhenOfTransactionTypeWithdrawal()
    {
        var character = new Character();
        var bankAccount = new BankAccount
        {
            Type = BankAccountType.Personal,
            OwningCharacter = character,
            BankCondition = new BankCondition
            {
                TransactionFeeBase = 0.20m,
                TransactionFeeMultiplier = 0.05m,
            }
        };
        
        var transaction = new BankAccountTransaction(bankAccount, BankAccountTransactionType.CashWithdrawal, 100);
        var booking = new BankAccountBooking(bankAccount, transaction);
        
        Assert.Equal(105.2m, booking.Amount);
    }
    
    [Fact]
    public void Amount_AddsFeesToAmount_WhenOfTransactionTypeTransferAndTypeOutgoing()
    {
        var character = new Character();
        var bankAccount = new BankAccount
        {
            Type = BankAccountType.Personal,
            OwningCharacter = character,
            BankCondition = new BankCondition
            {
                TransactionFeeBase = 0.20m,
                TransactionFeeMultiplier = 0.05m,
            }
        };
        
        var targetBankAccount = new BankAccount
        {
            Type = BankAccountType.Personal,
            OwningCharacter = character,
            BankCondition = new BankCondition
            {
                TransactionFeeBase = 1m,
                TransactionFeeMultiplier = 5m,
            }
        };
        
        var transaction = new BankAccountTransaction(bankAccount, targetBankAccount, 100);
        var booking = new BankAccountBooking(bankAccount, transaction);
        
        Assert.Equal(105.2m, booking.Amount);
    }
    
    [Fact]
    public void Amount_ReturnsTransactionAmount_WhenOfTransactionTypeTransferAndTypeIncoming()
    {
        var character = new Character();
        var sourceBankAccount = new BankAccount
        {
            Type = BankAccountType.Personal,
            OwningCharacter = character,
            BankCondition = new BankCondition
            {
                TransactionFeeBase = 0.20m,
                TransactionFeeMultiplier = 0.05m,
            }
        };
        
        var bankAccount = new BankAccount
        {
            Type = BankAccountType.Personal,
            OwningCharacter = character,
            BankCondition = new BankCondition
            {
                TransactionFeeBase = 1m,
                TransactionFeeMultiplier = 5m,
            }
        };
        
        var transaction = new BankAccountTransaction(bankAccount, sourceBankAccount, 100);
        var booking = new BankAccountBooking(sourceBankAccount, transaction);
        
        Assert.Equal(100m, booking.Amount);
    }
}
