using Domain.Shared;

namespace Application.Abstractions;

public interface INotificationScheduler
{
    Task ScheduleDailyReminderAsync(
        UserId userId,
        TimeOnly localTime,
        string timeZoneId,
        CancellationToken ct = default);

    Task CancelDailyReminderAsync(
        UserId userId,
        CancellationToken ct = default);
}