using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Core.Api.Models;
using ELifeRPG.Domain.Characters;

namespace ELifeRPG.Core.Api.Characters;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<Character, CharacterDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name!.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name!.LastName))
            .ReverseMap();
        
        CreateMap<CreateCharacterResult, ResultDto<CharacterDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s.Character));
    }
}
