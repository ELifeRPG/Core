﻿using ELifeRPG.Domain.Banking.Events;
using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;
using ELifeRPG.Domain.Countries;

namespace ELifeRPG.Domain.Banking;

public class Bank : EntityBase, IHasDomainEvents
{
    private static readonly Random Random = new();
    private readonly List<BankAccount>? _accounts = new();

    internal Bank()
    {
    }

    public Bank(Country country, int? number = null)
    {
        Country = country;
        Conditions = new List<BankCondition> { BankCondition.Default };
        Number = GenerateNumber(number, country);
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    
    public Country Country { get; init; } = null!;
    
    /// <summary>
    /// The national identification number of the bank.
    /// </summary>
    public int Number { get; init; }
    
    public ICollection<BankCondition>? Conditions { get; init; }

    public IReadOnlyCollection<BankAccount>? Accounts => _accounts;

    public BankAccount OpenAccount(Character owningCharacter)
    {
        var account = new BankAccount(this, owningCharacter);
        HandleNewAccount(account);
        return account;
    }
    
    public BankAccount OpenAccount(Company owningCompany)
    {
        var account = new BankAccount(this, owningCompany);
        HandleNewAccount(account);
        return account;
    }

    private void HandleNewAccount(BankAccount account)
    {
        _accounts!.Add(account);
        DomainEvents.Add(new BankAccountOpenedEvent(account));
    }

    public List<DomainEvent> DomainEvents { get; } = new();

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
        } while (true);
    }
}
