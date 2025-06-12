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
        var group = app
            .MapGroup("v1")
            .WithGroupName("v1")
            .WithTags(Tag);

        group
            .MapGet(
                "accounts/{accountId:guid}/characters",
                async (Guid accountId, IMediator mediator, CancellationToken cancellationToken) =>
                    {
                        var response = await mediator.Send(new CharactersQuery(accountId), cancellationToken);
                        return response.Match(
                            characters => Results.Ok(CharacterListDto.Create(characters)));
                    })
            .Produces<ResultDto<List<CharacterDto>>>()
            .WithTags(Tag)
            .WithSummary("List characters of the given account.");

        group
            .MapPost(
                "characters",
                async ([FromBody] CharacterDto character, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<ResultDto<CharacterDto>>(await mediator.Send(new CreateCharacterRequest(mapper.Map<Character>(character)), cancellationToken))))
            .Produces<ResultDto<CharacterDto>>()
            .WithTags(Tag)
            .WithSummary("Create new character.");

        group
            .MapPost(
                "characters/{characterId:guid}/sessions",
                async (Guid characterId, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<ResultDto<CharacterDto>>(await mediator.Send(new CreateCharacterSessionRequest(characterId), cancellationToken))))
            .Produces<ResultDto<CharacterDto>>()
            .WithTags(Tag)
            .WithSummary("Begins a new character session.");

        return app;
    }
}
