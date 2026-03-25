using Application.Abstractions;
using Application.Preferences.Commands;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Preferences.Handlers;

public sealed class UpdateUserPreferencesHandler(IAppUserRepository appUsers) : IRequestHandler<UpdateUserPreferencesCommand>
{
    public async Task Handle(UpdateUserPreferencesCommand command, CancellationToken ct = default)
    {
        var appUser = await appUsers.GetByUserIdAsync(command.UserId, ct);

        if (appUser is null)
        {
            appUser = AppUser.CreateNew(UserId.New());
            appUser.SetScriptureMode(command.ScriptureModeEnabled);
            await appUsers.AddAsync(appUser, ct);
        }
        else
        {
            appUser.SetScriptureMode(command.ScriptureModeEnabled);
            await appUsers.UpdateAsync(appUser, ct);
        }
    }
}