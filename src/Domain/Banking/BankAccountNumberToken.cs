using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking;

/// <summary>
/// Represents the rearranged structure of an IBAN with ASCII characters: (Bank-Code)(Account-Number)(Country-Code)(Checksum)
/// </summary>
public class BankAccountNumberToken
{
    public BankAccountNumberToken(string countryCode, int bankCode, long accountNumber)
    {
        Value = BuildValue(countryCode, $"{bankCode}{accountNumber}");
    }
    
    public BankAccountNumberToken(string countryCode, string remainder)
    {
        Value = BuildValue(countryCode, remainder);
    }

    public BankAccountNumberToken(decimal token)
    {
        Value = token;
    }
    
    public decimal Value { get; }
    
    public bool IsValidMod97()
    {
        return Value % 97 == 1;
    }

    private static decimal BuildValue(string countryCode, string remainder, byte? checksum = null)
    {
        var rearrangedValue = $"{remainder}{countryCode}{checksum}";
        var decimalValueString = rearrangedValue.Aggregate(string.Empty, (current, character) => current + (char.IsLetter(character) ? character - 55 : character - 48));
        if (!decimal.TryParse(decimalValueString, out var decimalValue))
        {
            throw new ELifeInvalidOperationException("Generated decimal value could not be parsed.");
        }

        return decimalValue;
    }
}
