using System.Globalization;
using System.Text;
using ELifeRPG.Domain.Banking.Internals;
using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking;

/// <summary>
/// Over-simplified version of an IBAN.
/// </summary>
public class BankAccountNumber
{
    public BankAccountNumber(Bank bank)
    {
        var randomNumber = Random.Shared.NextInt64(1000000000, 99999999999);
        var checkNumber = BuildCheckNumber(bank.Country.Code, bank.Number, randomNumber);
        
        Value = BuildValue(bank.Country.Code, checkNumber, bank.Number, randomNumber);
        Validate();
    }

    public BankAccountNumber(string countryCode, byte checkNumber, int bankCode, long accountNumber)
    {
        Value = BuildValue(countryCode, checkNumber, bankCode, accountNumber);
        Validate();
    }

    public BankAccountNumber(string bankAccountNumber)
    {
        Value = bankAccountNumber
            .ToUpper(CultureInfo.InvariantCulture)
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty);
    }

    public string Value { get; }

    public bool TryValidate()
    {
        try
        {
            Validate();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public bool TryValidate(out Exception? exception)
    {
        try
        {
            Validate();
            exception = null;
            return true;
        }
        catch (Exception thrownException)
        {
            exception = thrownException;
            return false;
        }
    }

    /// <summary>
    /// Prints the bank account number in a human readable format.
    /// </summary>
    public override string ToString()
    {
        var newStr = new StringBuilder();

        for (int i = 0; i < Value.Length; i++)
        {
            if (i > 0 && i % 4 == 0)
            {
                newStr.Append(' ');
            }

            newStr.Append(Value[i]);
        }
        
        return newStr.ToString();
    }
    
    private static byte BuildCheckNumber(string countryCode, int bankCode, long accountNumber)
    {
        var baseNumberToken = new BankAccountNumberToken(countryCode, bankCode, accountNumber);
        var checkNumber = 98 - (baseNumberToken.Value % 97);
        return (byte)checkNumber;
    }

    private static string BuildValue(string countryCode, byte checkNumber, int bankCode, long accountNumber)
    {
        var paddedCheckNumber = checkNumber < 10 ? $"0{checkNumber}" : checkNumber.ToString();
        return $"{countryCode}{paddedCheckNumber}{bankCode}{accountNumber}";
    }
    
    private void Validate()
    {
        //// IBAN can not contain more than 34 characters
        if (Value.Length > 34)
        {
            throw new ELifeInvalidOperationException("Value exceeds limit of 34 characters.");
        }

        if (!new BankAccountNumberToken(Value).IsValidMod97())
        {
            throw new ELifeInvalidOperationException("Generated bank account number did not pass MOD-97-10 ISO/IEC 7064:2003 check.");
        }
    }
}
