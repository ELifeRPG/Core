using ELifeRPG.Domain.Banking;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Banking;

public class BankAccountNumberTests
{
    /// <summary>
    /// See: https://www.iban.com/testibans
    /// </summary>
    [Theory]
    [InlineData("GB33 BUKB 2020 1555 5555 55", true, "Valid IBAN, length, checksum, bank code, account and structure")]
    [InlineData("GB94 BARC 1020 1530 0934 59", true, "Valid IBAN, bank code not found (bank cannot be identified): Valid length, checksum, bank code, account and structure")]
    [InlineData("GB94 BARC 2020 1530 0934 59", false, "Invalid IBAN check digits MOD-97-10 as per ISO/IEC 7064:2003")]
    public void Validates_CommonIBANRules(string bankAccountNumber, bool shouldBeValid, string reason)
    {
        var instance = new BankAccountNumber(bankAccountNumber);
        var isValid = instance.TryValidate();
        Assert.True(shouldBeValid == isValid, $"Is `{isValid}` but should be `{shouldBeValid}` - Reason: {reason}");
    }

    [Theory]
    [InlineData("EL28 9243 5275 2759 5717 2774", true)]
    public void Validates_ELifeRPGRules(string bankAccountNumber, bool shouldBeValid)
    {
        var instance = new BankAccountNumber(bankAccountNumber);
        Assert.Equal(shouldBeValid, instance.TryValidate());
    }

    [Theory]
    [InlineData("EL", 31, 71302, 402, "EL3171302402")]
    public void ConstructsValidBankAccountNumber(string countryCode, byte checkNumber, int bankCode, long accountNumber, string assertedValue)
    {
        var instance = new BankAccountNumber(countryCode, checkNumber, bankCode, accountNumber);
        Assert.Equal(assertedValue, instance.Value);
    }

    [Fact]
    public void ToString_FormatsToHumanReadableString()
    {
        var instance = new BankAccountNumber("GB33BUKB20201555555555");
        Assert.Equal("GB33 BUKB 2020 1555 5555 55", instance.ToString());
    }
}
