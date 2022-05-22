using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Core.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class AccountEndpoints
{
    public static WebApplication MapAccountEndpoints(this WebApplication app)
    {
        app
            .MapGet(
                "/accounts/{accountId:Guid}/characters", 
                async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromRoute] Guid accountId, CancellationToken cancellationToken) => 
                    Results.Ok(mapper.Map<List<CharacterDto>>((await mediator.Send(new ListCharactersQuery(accountId), cancellationToken)).Characters)))
            .Produces<List<CharacterDto>>();
        
        return app;
    }
}
