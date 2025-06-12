using System.Text.Json.Serialization;
using ELifeRPG.Application;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Primitives;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddAutoMapper(options => options.AllowNullCollections = true, typeof(Program));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration,
    builder.Environment,
    tracingBuilder: x =>
    {
        x.AddAspNetCoreInstrumentation();
    });

builder.Services.AddOpenApi(
    "v1",
    options =>
    {
        options.ShouldInclude = description => description.GroupName == "v1";
    });

var app = builder.Build();

//// quick fix for Reforgers disability to modify request headers / body encoding
app.Use(async (context, next) =>
{
    if (context.Request.Method.Equals("post", StringComparison.OrdinalIgnoreCase) &&
        context.Request.Headers.ContentType.Equals("application/x-www-form-urlencoded"))
    {
        context.Request.Headers.ContentType = new StringValues("application/json");
    }

    await next(context);
});

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(
        "/docs",
        options =>
        {
            options.WithDynamicBaseServerUrl();
            options.AddDocuments("v1");
        });
}

app.UseRouting();
app
    .MapInternalEndpoints()
    .MapAccountEndpoints()
    .MapBankingEndpoints()
    .MapCharacterEndpoints()
    .MapCompanyEndpoints();

app.Run();
