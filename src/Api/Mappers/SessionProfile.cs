using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Models;

namespace ELifeRPG.Core.Api.Mappers;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<CreateSessionResponse, SessionDto>()
            .ForMember(d => d.AccountId, o => o.MapFrom(s => s.AccountId));

        CreateMap<CreateSessionResponse, ResultResultDto<SessionDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s));
        
        CreateMap<CreateCharacterSessionResponse, ResultResultDto<CharacterDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s.Character));
    }
}
