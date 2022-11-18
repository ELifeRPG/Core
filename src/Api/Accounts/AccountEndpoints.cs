using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Accounts;
using ELifeRPG.Core.Api.Characters;
using ELifeRPG.Core.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class AccountEndpoints
{
    public const string Tag = "Account";
    
    public static WebApplication MapAccountEndpoints(this WebApplication app)
    {
        app
            .MapPost(
                "/sessions",
                async ([FromBody] SessionRequestDto sessionRequest, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
                    => Results.Ok(mapper.Map<ResultDto<SessionDto>>(await mediator.Send(new CreateSessionRequest(sessionRequest.SteamId), cancellationToken))))
            .Produces<ResultDto<SessionDto>>();
        
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
