using Api.Models;
using AutoMapper;
using ELifeRPG.Application;
using ELifeRPG.Application.Characters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(Application));
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/characters", async ([FromServices] IMediator mediator, [FromServices] IMapper mapper, CancellationToken cancellationToken) => Results.Ok(mapper.Map<List<CharacterDto>>((await mediator.Send(new ListCharactersQuery(), cancellationToken)).Characters)));
app.Run();
