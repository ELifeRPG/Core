using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Core.Api.Models;
using ELifeRPG.Domain.Characters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class CharacterEndpoints
{
    public static WebApplication MapCharacterEndpoints(this WebApplication app)
    {
        app
            .MapPost(
                "/characters", 
                async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] CharacterDto character, CancellationToken cancellationToken) =>
                    Results.Ok(mapper.Map<CharacterDto>((await mediator.Send(new CreateCharacterRequest(mapper.Map<Character>(character)), cancellationToken)).Character)))
            .Produces<CharacterDto>();
        
        app
            .MapPost(
                "/characters/{characterId:Guid}/sessions", 
                async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromRoute] Guid characterId, CancellationToken cancellationToken) =>
                    Results.Ok(mapper.Map<SessionDto>(await mediator.Send(new CreateCharacterSessionRequest(characterId), cancellationToken))))
            .Produces<SessionDto>();
        
        return app;
    }
}
