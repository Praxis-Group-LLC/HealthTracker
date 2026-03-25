namespace Application.DTOs;

public sealed record UpdatePreferencesRequest(
    string TimeZoneId,
    bool ScriptureModeEnabled);