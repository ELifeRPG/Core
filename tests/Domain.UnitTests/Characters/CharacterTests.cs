using System.Linq;
using ELifeRPG.Domain.Characters;
using Xunit;

namespace ELifeRPG.Core.Domain.UnitTests.Characters;

public class CharacterTests
{
    [Fact]
    public void CreatingCharacterFromCharacterInfo_ShouldRaiseCharacterCreatedEvent()
    {
        var character = new Character(new Character());

        Assert.Equal(1, character.DomainEvents.Count(x => x.GetType() == typeof(CharacterCreatedEvent)));
    }
}
