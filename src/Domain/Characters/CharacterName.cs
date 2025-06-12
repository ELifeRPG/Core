namespace ELifeRPG.Domain.Characters;

public class CharacterName
{
    public CharacterName()
    {
    }

    public CharacterName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;
}
