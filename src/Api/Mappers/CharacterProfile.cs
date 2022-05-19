using Api.Models;
using AutoMapper;
using ELifeRPG.Domain.Characters;

namespace Api.Mappers;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<Character, CharacterDto>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Name.FirstName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.Name.LastName))
            .ReverseMap();
    }
}