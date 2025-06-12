using ELifeRPG.Domain.Banking.Events;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;
using ELifeRPG.Domain.Persons;

namespace ELifeRPG.Domain.Banking;

public class Bank : EntityBase, IHasDomainEvents
{
    private static readonly Random Random = new();
    private readonly List<BankAccount>? _accounts = new();

    public Bank(Country country, int? number = null)
    {
        Country = country;
        Conditions = new List<BankCondition> { BankCondition.Default };
        Number = GenerateNumber(number, country);
    }

    internal Bank()
    {
    }

    public Guid Id { get; init; } = Guid.NewGuid();

    public Country Country { get; init; } = null!;

    /// <summary>
    /// The national identification number of the bank.
    /// </summary>
    public int Number { get; init; }

    public ICollection<BankCondition>? Conditions { get; init; }

    public IReadOnlyCollection<BankAccount>? Accounts => _accounts;

    public List<DomainEvent> DomainEvents { get; } = new();

    public BankAccount OpenAccount(Person person)
    {
        var account = new BankAccount(this, person);
        HandleNewAccount(account);
        return account;
    }

    private static int GenerateNumber(int? givenNumber, Country country)
    {
        do
        {
            var number = givenNumber ?? Random.Next(10000, 99999);

            if (country.Banks!.Any(x => x.Number == number))
            {
                continue;
            }

            return number;
        }
        while (true);
    }

    private void HandleNewAccount(BankAccount account)
    {
        _accounts!.Add(account);
        DomainEvents.Add(new BankAccountOpenedEvent(account));
    }
}
