﻿using ELifeRPG.Domain.Characters;
using ELifeRPG.Domain.Common;
using ELifeRPG.Domain.Companies;

namespace ELifeRPG.Domain.Persons;

public enum PersonType
{
    Character = 1,
    Company = 2,
}

public class Person : EntityBase
{
    internal Person()
    {
    }
    
    public Person(Character character)
    {
        Character = character;
        Type = PersonType.Character;
    }

    public Person(Company company)
    {
        Company = company;
        Type = PersonType.Company;
    }

    public Guid Id { get; init; } = Guid.NewGuid();
    
    public PersonType Type { get; init; }
    
    public Character? Character { get; init; }
    
    public Company? Company { get; init; }
}
