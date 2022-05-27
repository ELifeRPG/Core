using AspNet.Security.OpenId.Steam;
using ELifeRPG.Application;
using ELifeRPG.Core.WebUI.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.DevelopmentPrivate.json", true); // store your api key for dev purposes ;)
}

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
        options.ApplicationKey = builder.Configuration.GetValue<string>("ELifeRPG:OIDC:SteamApiKey");
    });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.Scan(scanner => scanner
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo<ViewModelBase>())
        .AsSelf()
        .WithTransientLifetime());
builder.Services.AddMudServices();

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
