using System.Reflection;
using System.Text.Json.Serialization;
using ELifeRPG.Application;
using ELifeRPG.Core.Api.OpenAPI;
using Microsoft.AspNetCore.Http.Json;

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

builder.Services.AddAutoMapper(options =>options.AllowNullCollections = true, typeof(Program));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .MapInternalEndpoints()
    .MapAccountEndpoints()
    .MapBankingEndpoints()
    .MapCharacterEndpoints()
    .MapCompanyEndpoints();

app.Run();
