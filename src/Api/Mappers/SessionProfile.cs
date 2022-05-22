using AutoMapper;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Models;

namespace ELifeRPG.Core.Api.Mappers;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        CreateMap<CreateSessionResponse, SessionDto>()
            .ForMember(d => d.AccountId, o => o.MapFrom(s => s.AccountId));
    }
}
