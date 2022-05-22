using System.Net;
using AutoMapper;
using ELifeRPG.Application.Characters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class CharacterEndpoints
{
    public static WebApplication MapCharacterEndpoints(this WebApplication app)
    {
        app
            .MapPost(
                "/characters/{characterId:Guid}/sessions", 
                async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromRoute] Guid characterId, CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new CreateCharacterSessionRequest(characterId), cancellationToken);
                    return Results.NoContent();
                })
            .Produces((int)HttpStatusCode.NoContent);
        
        return app;
    }
}
