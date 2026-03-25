using Application.Abstractions;
using Application.DTOs;
using Application.Preferences.Queries;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Preferences.Handlers;

public sealed class GetUserPreferencesHandler(IAppUserRepository appUsers) : IRequestHandler<GetUserPreferencesQuery, UserPreferencesDto>
{
    public async Task<UserPreferencesDto> Handle(
        GetUserPreferencesQuery query,
        CancellationToken ct = default)
    {
        var profile = await appUsers.GetByUserIdAsync(query.UserId, ct);

        if (profile is null)
        {
            // default profile (e.g. first-time user)
            profile = AppUser.CreateNew(UserId.New());
            await appUsers.AddAsync(profile, ct);
        }

        return UserPreferencesDto.FromDomain(profile);
    }
}