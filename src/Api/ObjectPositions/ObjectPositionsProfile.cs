using AutoMapper;
using ELifeRPG.Domain.ObjectPositions;

namespace ELifeRPG.Core.Api.ObjectPositions;

public class ObjectPositionsProfile : Profile
{
    public ObjectPositionsProfile()
    {
        CreateMap<PositionData, PositionDataDto>();
        CreateMap<EnfusionVector, EnfusionVectorDto>();
        CreateMap<EnfusionQuaternion, EnfusionQuaternionDto>();
    }
}
