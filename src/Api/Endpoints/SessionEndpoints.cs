using AutoMapper;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ELifeRPG.Core.Api.Endpoints;

public static class SessionEndpoints
{
    public static WebApplication MapSessionEndpoints(this WebApplication app)
    {
        app
            .MapPost(
                "/sessions", 
                async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, [FromBody] SessionDto session, CancellationToken cancellationToken) =>
                    Results.Ok(mapper.Map<SessionDto>(await mediator.Send(new CreateSessionRequest(session.EnfusionIdentifier!), cancellationToken))))
            .Produces<SessionDto>();
        
        return app;
    }
}
