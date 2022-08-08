﻿using ELifeRPG.Application.Common.Exceptions;

namespace ELifeRPG.Domain.Banking.Accounts;

/// <summary>
/// Over-simplified version of an IBAN.
/// </summary>
public class BankAccountNumber
{
    public BankAccountNumber(string countryCode, byte checkNumber, int bankCode, long accountNumber)
    {
        Value = $"{countryCode}{checkNumber}{bankCode}{accountNumber}";
        Validate();
    }

    public BankAccountNumber(string bankAccountNumber)
    {
        Value = bankAccountNumber
            .ToUpper()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty);
    }

    public string Value { get; }

    public static BankAccountNumber Generate(Bank bank)
    {
        var randomNumber = new Random().Next(100000, 999999);
        return new BankAccountNumber("EL", 31, bank.Number, randomNumber);
    }

    public void Validate()
    {
        //// EBAN can not contain more than 34 characters
        if (Value.Length > 34)
        {
            throw new ELifeInvalidOperationException("Value exceeds limit of 34 characters.");
        }

        var rearrangedValue = $"{Value[4..]}{Value[..4]}";
        var decimalValueString = rearrangedValue.Aggregate(string.Empty, (current, character) => current + (char.IsLetter(character) ? character - 55 : character - 48));
        if (!decimal.TryParse(decimalValueString, out var decimalValue))
        {
            throw new ELifeInvalidOperationException("Generated decimal value could not be parsed.");
        }

        if ((decimalValue % 97) == 1)
        {
            throw new ELifeInvalidOperationException("Generated bank account number did not pass MOD-97-10 ISO/IEC 7064:2003 check.");
        }
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
