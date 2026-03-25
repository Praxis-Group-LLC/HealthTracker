using Application.Abstractions;
using Domain.Shared;

namespace Application.Preferences.Commands;

public sealed record UpdateUserPreferencesCommand(
    UserId UserId,
    bool ScriptureModeEnabled
    ) : ICommand;