﻿using System.Net;
using System.Security.Claims;
using AspNet.Security.OAuth.Discord;
using ELifeRPG.Application.Accounts;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ELifeRPG.Core.WebUI.Pages.Authentication;

[AllowAnonymous]
public class Signin : PageModel
{
    private readonly IMediator _mediator;

    public Signin(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGet([FromQuery] string? provider, [FromQuery] string? callback, [FromQuery] string? redirectUri)
    {
        if (string.IsNullOrEmpty(provider) && string.IsNullOrEmpty(callback))
        {
            provider = DiscordAuthenticationDefaults.AuthenticationScheme;
        }
        
        if (provider?.Equals(DiscordAuthenticationDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase) ?? false)
        {
            var uri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}{HttpContext.Request.Path}?callback={DiscordAuthenticationDefaults.AuthenticationScheme}";
            if (!string.IsNullOrEmpty(redirectUri))
            {
                uri += $"&redirectUri={redirectUri}";
            }
            
            return Challenge(new AuthenticationProperties { RedirectUri = uri }, DiscordAuthenticationDefaults.AuthenticationScheme);
        }
        
        if (callback?.Equals(DiscordAuthenticationDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase) ?? false)
        {
            var identity = User.Identities.First();
            var nameIdentifierClaim = identity.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier);
            var nameClaim = identity.Claims.Single(x => x.Type == ClaimTypes.Name);
            await _mediator.Send(new UserSignedInRequest(long.Parse(nameIdentifierClaim.Value), nameClaim.Value));
            return Redirect($"/{redirectUri ?? string.Empty}");
        }

        return StatusCode((int)HttpStatusCode.NoContent);
    }
}
