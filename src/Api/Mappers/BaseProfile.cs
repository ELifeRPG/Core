using AutoMapper;
using ELifeRPG.Application.Common;
using ELifeRPG.Core.Api.Models;

namespace ELifeRPG.Core.Api.Mappers;

public class BaseProfile : Profile
{
    public BaseProfile()
    {
        CreateMap<ResponseBase, ResultDtoBase>()
            .ForMember(d => d.Messages, o => o.MapFrom(s => s.Messages))
            .IncludeAllDerived();
    }
}
