using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking;

public static class BankAccountNumberGenerator
{
    private static readonly Random Random = new();
    
    /// <summary>
    /// Generates a valid account number for the given bank.
    /// </summary>
    public static BankAccountNumber Generate(Bank bank)
    {
        for (var i = 1; i <= 3; i++)
        {
            var randomNumber = Random.NextInt64(1000000000, 99999999999);
            var checkNumber = BuildCheckNumber(bank.Country.Code, bank.Number, randomNumber);
            var bankAccountNumber = new BankAccountNumber(bank.Country.Code, checkNumber, bank.Number, randomNumber);
            if (bankAccountNumber.TryValidate())
            {
                return bankAccountNumber;
            }

            if (i == 3)
            {
                throw new ELifeInvalidOperationException($"Could not generate bank account number - attempts: {i}");
            }
        }

        throw new ELifeInvalidOperationException("Could not generate bank account number");
    }

    private static byte BuildCheckNumber(string countryCode, int bankCode, long accountNumber)
    {
        var baseNumberToken = new BankAccountNumberToken(countryCode, bankCode, accountNumber);
        var checkNumber = 98 - (baseNumberToken.Value % 97);
        return (byte)checkNumber;
    }
}
