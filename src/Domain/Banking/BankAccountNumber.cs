using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking;

/// <summary>
/// Over-simplified version of an IBAN.
/// </summary>
public class BankAccountNumber
{
    public BankAccountNumber(string countryCode, byte checkNumber, int bankCode, long accountNumber)
    {
        Value = $"{countryCode}{checkNumber}{bankCode}{accountNumber}";
    }

    public BankAccountNumber(string bankAccountNumber)
    {
        Value = bankAccountNumber
            .ToUpper()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty);
    }

    public string Value { get; }

    public void Validate()
    {
        //// EBAN can not contain more than 34 characters
        if (Value.Length > 34)
        {
            throw new ELifeInvalidOperationException("Value exceeds limit of 34 characters.");
        }

        if (!new BankAccountNumberToken(Value[..4], Value[4..]).IsValidMod97())
        {
            throw new ELifeInvalidOperationException("Generated bank account number did not pass MOD-97-10 ISO/IEC 7064:2003 check.");
        }
    }

    public static bool DecimalTokenIsValidMod971ß(decimal token)
    {
        return token % 97 == 1;
    }

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
        var newStr = string.Empty;

        for (int i = 0; i < Value.Length; i++)
        {
            if (i > 0 && i % 4 == 0)
            {
                newStr += " ";
            }

            newStr += Value[i];
        }
        
        return newStr;
    }
}
