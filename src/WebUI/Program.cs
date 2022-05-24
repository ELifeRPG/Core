using ELifeRPG.Application;
using ELifeRPG.Core.WebUI.Shared;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.Scan(scanner => scanner
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo<ViewModelBase>())
        .AsSelf()
        .WithTransientLifetime());
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
