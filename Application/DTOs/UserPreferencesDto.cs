using Domain.Users;

namespace Application.DTOs;

public sealed record UserPreferencesDto(bool ScriptureModeEnabled)
{
    public static UserPreferencesDto FromDomain(AppUser user) =>
        new(user.ScriptureModeEnabled);
}