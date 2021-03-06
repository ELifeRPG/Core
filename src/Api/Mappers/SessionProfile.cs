using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Models;

namespace ELifeRPG.Core.Api.Mappers;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<CreateSessionResult, SessionDto>()
            .ForMember(d => d.AccountId, o => o.MapFrom(s => s.AccountId));

        CreateMap<CreateSessionResult, ResultDto<SessionDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s));
        
        CreateMap<CreateCharacterSessionResult, ResultDto<CharacterDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s.Character));
    }
}
