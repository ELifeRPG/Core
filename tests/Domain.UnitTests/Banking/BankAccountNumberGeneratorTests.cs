using ELifeRPG.Domain.Banking;
using ELifeRPG.Domain.Countries;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Banking;

public class BankAccountNumberGeneratorTests
{
    [Fact]
    public void Generate_GeneratesValidNumbers()
    {
        var bank = new Bank { Country = Country.Default, Number = 924352752 };

        for (var i = 0; i <= 1; i++)
        {
            var bankAccountNumber = BankAccountNumberGenerator.Generate(bank);
            Assert.NotEmpty(bankAccountNumber.Value);
            Assert.True(bankAccountNumber.TryValidate(), $"Could not validate number `{bankAccountNumber}`");
        }
    }
}
