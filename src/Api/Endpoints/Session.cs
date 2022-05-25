using AutoMapper;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class Session
{
    public static WebApplication MapSessionEndpoints(this WebApplication app)
    {
        app
            .MapPost("/sessions", CreateSession)
            .Produces<ResultDto<SessionDto>>();
        
        return app;
    }

    /// <summary>
    /// Starts a new session.
    /// </summary>
    /// <param name="session">The session object which contains the needed information to create a session.
    /// For Enfusion, the <seealso cref="SessionDto.SteamId"/> is required.</param>
    private static async Task<IResult> CreateSession([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] SessionDto session, CancellationToken cancellationToken)
    {
        return Results.Ok(mapper.Map<ResultDto<SessionDto>>(await mediator.Send(new CreateSessionRequest(session.SteamId!.Value), cancellationToken)));
    }
}
