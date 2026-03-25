using System.Security.Claims;
using Domain.Shared;

namespace Backend.Security;

public static class UserExtensions
{
    public static UserId GetUserId(this ClaimsPrincipal user)
    {
        var idValue = user.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? user.FindFirstValue("userId");

        if (idValue is null || !Guid.TryParse(idValue, out var guid))
        {
            throw new InvalidOperationException("Authenticated user has no valid user id claim.");
        }

        return new UserId(guid);
    }
}