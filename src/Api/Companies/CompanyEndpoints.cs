﻿using AutoMapper;
using ELifeRPG.Application.Companies;
using ELifeRPG.Core.Api.Companies;
using ELifeRPG.Core.Api.Models;
using Mediator;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

public static class CompanyEndpoints
{
    public const string Tag = "Companies";

    public static WebApplication MapCompanyEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("v1")
            .WithGroupName("v1")
            .WithTags(Tag);

        group
            .MapGet("companies", GetCompanies)
            .Produces<ResultDto<List<CompanyDto>>>();

        return app;
    }

    /// <summary>
    /// Returns all companies.
    /// </summary>
    private static async Task<IResult> GetCompanies([FromServices] IMediator mediator, [FromServices] IMapper mapper, CancellationToken cancellationToken)
    {
        return Results.Ok(mapper.Map<ResultDto<List<CompanyDto>>>(await mediator.Send(new ListCompaniesQuery(), cancellationToken)));
    }
}
