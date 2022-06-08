using AspNet.Security.OpenId.Steam;
using ELifeRPG.Application;
using ELifeRPG.Application.Common;
using ELifeRPG.Core.WebUI.Pages.Companies.Details;
using ELifeRPG.Core.WebUI.Shared;
using ELifeRPG.Infrastructure.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor;
using MudBlazor.Services;
using MvvmBlazor.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = SteamAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "eliferpg";
        options.LoginPath = "/authentication/sign-in";
        options.LogoutPath = "/authentication/sign-out";
    })
    .AddSteam(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ApplicationKey = builder.Configuration.GetValue<string>("ELifeRPG:SteamApiKey");
    });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddMvvm();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMudServices();

builder.Services.Scan(scanner => scanner
    .FromAssemblyOf<Program>()
    .AddClasses(x => x.AssignableTo<ViewModelBase>()).AsSelf().WithTransientLifetime());

builder.Services.AddScoped<ISettingsStore, SettingsStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
