using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking;

/// <summary>
/// Over-simplified version of an IBAN.
/// </summary>
public class BankAccountNumber
{
    public BankAccountNumber(string countryCode, byte checkNumber, int bankCode, long accountNumber)
    {
        var paddedCheckNumber = checkNumber < 10 ? $"0{checkNumber}" : checkNumber.ToString();
        Value = $"{countryCode}{paddedCheckNumber}{bankCode}{accountNumber}";
    }

    public BankAccountNumber(string bankAccountNumber)
    {
        Value = bankAccountNumber
            .ToUpper()
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
