using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Core.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class Account
{
    public static WebApplication MapAccountEndpoints(this WebApplication app)
    {
        app
            .MapGet("/accounts/{accountId:Guid}/characters", GetCharacters)
            .Produces<ResultDto<List<CharacterDto>>>();
        
        return app;
    }

    /// <summary>
    /// Returns characters for the given account.
    /// </summary>
    /// <param name="accountId">The Id of the account of which from the characters will be loaded.</param>
    private static async Task<IResult> GetCharacters([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromRoute] Guid accountId, CancellationToken cancellationToken)
    {
        return Results.Ok(mapper.Map<ResultDto<List<CharacterDto>>>(await mediator.Send(new ListCharactersQuery(accountId), cancellationToken)));
    }
}
