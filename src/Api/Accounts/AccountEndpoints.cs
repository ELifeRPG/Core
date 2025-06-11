using AutoMapper;
using ELifeRPG.Application.Characters;
using ELifeRPG.Application.Sessions;
using ELifeRPG.Core.Api.Accounts;
using ELifeRPG.Core.Api.Characters;
using ELifeRPG.Core.Api.Models;
using Mediator;
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
                    => Results.Ok(mapper.Map<ResultDto<SessionDto>>(await mediator.Send(new CreateSessionRequest(sessionRequest.BohemiaId), cancellationToken))))
            .Produces<ResultDto<SessionDto>>();
        
        return app;
    }
}
