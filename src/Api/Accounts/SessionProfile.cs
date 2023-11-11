using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Characters;
using ELifeRPG.Core.Api.Models;
using ELifeRPG.Domain.Accounts;

namespace ELifeRPG.Core.Api.Accounts;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<CreateSessionResult, SessionDto>()
            .ForMember(d => d.AccountId, o => o.MapFrom(s => s.AccountId))
            .ForMember(d => d.Locked, o => o.MapFrom(s => s.AccountStatus == AccountStatus.Locked));

        CreateMap<CreateSessionResult, ResultDto<SessionDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s));
        
        CreateMap<CreateCharacterSessionResult, ResultDto<CharacterDto>>()
            .ForMember(d => d.Data, o => o.MapFrom(s => s.Character));
    }
}
