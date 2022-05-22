using AutoMapper;
using ELifeRPG.Application.Common;
using ELifeRPG.Core.Api.Models;

namespace ELifeRPG.Core.Api.Mappers;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>();
        CreateMap<MessageType, MessageTypeDto>();
    }
}
