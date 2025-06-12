using ELifeRPG.Core.Api.Models;
using ELifeRPG.Domain.Characters;

namespace ELifeRPG.Core.Api.Characters;

public class CharacterListDto : ResultDto<List<CharacterDto>>
{
    public static CharacterListDto Create(List<Character> characters)
        => new()
        {
            Data = characters
                .Select(CharacterDto.Create)
                .ToList(),
        };
}
