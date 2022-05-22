using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Core.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class Character
{
    public static WebApplication MapCharacterEndpoints(this WebApplication app)
    {
        app
            .MapPost("/characters", CreateCharacter)
            .Produces<ResultResultDto<CharacterDto>>();
        
        app
            .MapPost("/characters/{characterId:Guid}/sessions", CreateCharacterSession)
            .Produces<ResultResultDto<CharacterDto>>();
        
        return app;
    }

    /// <summary>
    /// Creates a character.
    /// </summary>
    private static async Task<IResult> CreateCharacter([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] CharacterDto character, CancellationToken cancellationToken)
    {
        return Results.Ok(mapper.Map<ResultResultDto<CharacterDto>>(await mediator.Send(new CreateCharacterRequest(mapper.Map<Domain.Characters.Character>(character)), cancellationToken)));
    }

    /// <summary>
    /// Begins a new character session.
    /// </summary>
    /// <param name="characterId">The Id of the used character.</param>
    private static async Task<IResult> CreateCharacterSession([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromRoute] Guid characterId, CancellationToken cancellationToken)
    {
        return Results.Ok(mapper.Map<ResultResultDto<CharacterDto>>(await mediator.Send(new CreateCharacterSessionRequest(characterId), cancellationToken)));
    }
}
