using ELifeRPG.Domain.Common.Exceptions;

namespace ELifeRPG.Domain.Banking.Internals;

/// <summary>
/// Represents the rearranged structure of an IBAN with ASCII characters: (Bank-Code)(Account-Number)(Country-Code)(Check-Number)
/// </summary>
internal class BankAccountNumberToken
{
    /// <summary>
    /// Constructs the token from Country-Code, Bank-Code and regular Account-Number.
    /// </summary>
    public BankAccountNumberToken(string countryCode, int bankCode, long accountNumber)
    {
        Value = BuildValue(countryCode, $"{bankCode}{accountNumber}", "00");
    }

    /// <summary>
    /// Constructs the token from the IBAN.
    /// </summary>
    public BankAccountNumberToken(string iban)
    {
        Value = BuildValue(iban[..2], iban[4..], iban[2..4]);
    }

    public decimal Value { get; }

    public bool IsValidMod97()
    {
        return Value % 97 == 1;
    }

    private static decimal BuildValue(string countryCode, string remainder, string? checkNumber = null)
    {
        var rearrangedValue = $"{remainder}{countryCode}{checkNumber ?? string.Empty}";
        var decimalValueString = rearrangedValue.Aggregate(string.Empty, (current, character) => current + (char.IsLetter(character) ? character - 55 : character - 48));

        if (!decimal.TryParse(decimalValueString, out var parsedValue))
        {
            throw new ELifeInvalidOperationException("Generated value could not be parsed.");
        }

        return parsedValue;
    }
}
