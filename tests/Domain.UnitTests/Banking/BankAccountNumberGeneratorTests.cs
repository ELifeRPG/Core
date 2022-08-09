using ELifeRPG.Domain.Banking;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Banking.Accounts;

public class BankAccountNumberGeneratorTests
{
    [Fact]
    public void Generate_GeneratesValidNumber()
    {
        var bank = new Bank { Number = 2435 };
        var bankAccountNumber = BankAccountNumberGenerator.Generate(bank);
        Assert.NotEmpty(bankAccountNumber.Value);
    }
}
