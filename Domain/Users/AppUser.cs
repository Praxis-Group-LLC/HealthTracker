using Domain.Shared;

namespace Domain.Users;

public sealed class AppUser
{
    public UserId UserId { get; init; }
    public bool ScriptureModeEnabled { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; init; }
    public DateTimeOffset LastSeenAtUtc { get; init; }
    
    // ReSharper disable once UnusedMember.Local
    private AppUser() { }

    private AppUser(
        UserId userId,
        bool scriptureModeEnabled)
    {
        UserId = userId;
        ScriptureModeEnabled = scriptureModeEnabled;
    }

    public static AppUser CreateNew(UserId userId)
    {
        return new AppUser(
            userId,
            scriptureModeEnabled: false);
    }

    public void SetScriptureMode(bool enabled)
    {
        ScriptureModeEnabled = enabled;
    }
}

