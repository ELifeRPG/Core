using System.Reflection;
using System.Text.Json.Serialization;
using ELifeRPG.Application;
using ELifeRPG.Core.Api.OpenAPI;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Primitives;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.SchemaFilter<EnumSchemaFilter>();
    options.EnableAnnotations();
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

var app = builder.Build();

//// quick fix for Reforgers disability to modify request headers / body encoding
app.Use(async (context, next) =>
{
    if (context.Request.Method.Equals("post", StringComparison.OrdinalIgnoreCase) &&
        context.Request.Headers.ContentType.Equals("x-www-form-urlencoded"))
    {
        context.Request.Headers.ContentType = new StringValues("application/json");
    }

    await next(context);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app
    .MapInternalEndpoints()
    .MapAccountEndpoints()
    .MapBankingEndpoints()
    .MapCharacterEndpoints()
    .MapCompanyEndpoints();

app.Run();
