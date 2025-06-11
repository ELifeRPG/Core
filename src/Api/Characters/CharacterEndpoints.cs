using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Core.Api.Characters;
using ELifeRPG.Core.Api.Models;
using ELifeRPG.Domain.Characters;
using Mediator;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class CharacterEndpoints
{
    public const string Tag = "Character";
    
    public static WebApplication MapCharacterEndpoints(this WebApplication app)
    {
        app
            .MapGet(
                "/accounts/{accountId:guid}/characters",
                async (Guid accountId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<ResultDto<List<CharacterDto>>>(await mediator.Send(new ListCharactersQuery(accountId), cancellationToken))))
            .Produces<ResultDto<List<CharacterDto>>>();
        
        app
            .MapPost(
                "/characters",
                async ([FromBody] CharacterDto character, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<ResultDto<CharacterDto>>(await mediator.Send(new CreateCharacterRequest(mapper.Map<Character>(character)), cancellationToken))))
            .Produces<ResultDto<CharacterDto>>()
            .WithTags(Tag)
            .WithSummary("Create new character.");
        
        app
            .MapPost(
                "/characters/{characterId:guid}/sessions",
                async (Guid characterId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<ResultDto<CharacterDto>>(await mediator.Send(new CreateCharacterSessionRequest(characterId), cancellationToken))))
            .Produces<ResultDto<CharacterDto>>()
            .WithTags(Tag)
            .WithSummary("Begins a new character session.");
        
        return app;
    }
}
