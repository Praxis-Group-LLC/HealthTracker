namespace Application.DTOs;

public sealed record UpdateReminderSettingsRequest(
    TimeOnly ReminderTime,
    bool Enabled);