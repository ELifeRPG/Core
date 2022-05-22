using ELifeRPG.Application;
using ELifeRPG.Core.Api.Endpoints;
using MediatR;

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

app.MapSessionEndpoints();
app.MapAccountEndpoints();
app.MapCharacterEndpoints();

app.Run();
