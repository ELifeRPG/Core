using AspNet.Security.OAuth.Discord;
using ELifeRPG.Application;
using ELifeRPG.Core.WebUI.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = DiscordAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.Cookie.Name = "eliferpg";
        options.LoginPath = "/authentication/sign-in";
        options.LogoutPath = "/authentication/sign-out";
    })
    .AddDiscord(options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.ClientId = builder.Configuration.GetValue<string>("OIDC:Discord:ClientId")!;
        options.ClientSecret = builder.Configuration.GetValue<string>("OIDC:Discord:ClientSecret")!;
    });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
});

builder.Services.AddMvvm();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddMudServices();

builder.Services.Scan(scanner => scanner
    .FromAssemblyOf<Program>()
    .AddClasses(x => x.AssignableTo<ViewModelBase>()).AsSelf().WithTransientLifetime());

builder.Services.AddScoped<ISettingsStore, SettingsStore>();
builder.Services.AddScoped<BreadcrumbsCollection>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.MapInternalEndpoints();

app.UseAuthentication();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
