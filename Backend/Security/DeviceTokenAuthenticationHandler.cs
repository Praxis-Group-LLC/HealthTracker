using System.Security.Claims;
using System.Text.Encodings.Web;
using Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Backend.Security;

public sealed class DeviceTokenAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory loggerFactory,
    UrlEncoder encoder,
    IDeviceAuthService deviceAuth)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, loggerFactory, encoder)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Allow anonymous for endpoints that don't require auth
        if (!Request.Headers.TryGetValue(DeviceTokenAuthenticationDefaults.HeaderName, out var headerValues))
        {
            return AuthenticateResult.NoResult();
        }

        var token = headerValues.ToString();
        if (string.IsNullOrWhiteSpace(token))
        {
            return AuthenticateResult.Fail("Device token is empty.");
        }

        var userId = await deviceAuth.ValidateTokenAsync(token, Context.RequestAborted);
        if (userId is null)
        {
            return AuthenticateResult.Fail("Invalid or revoked device token.");
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()),
            new Claim("userId", userId.Value.ToString())
        };

        var identity = new ClaimsIdentity(claims, DeviceTokenAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, DeviceTokenAuthenticationDefaults.AuthenticationScheme);

        return AuthenticateResult.Success(ticket);
    }
}
