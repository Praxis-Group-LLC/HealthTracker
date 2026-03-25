using Domain.Shared;

namespace Domain.Coping;

public sealed class CopingSession
{
    public CopingSessionId Id { get; init; }
    public UserId UserId { get; init; }
    public CopingExerciseId CopingExerciseId { get; init; }
    public DateTimeOffset StartedAt { get; init; }
    public DateTimeOffset? CompletedAt { get; private set; }
    
    // ReSharper disable once UnusedMember.Local
    private CopingSession() { }

    private CopingSession(UserId userId, CopingExerciseId copingExerciseId, DateTimeOffset startedAt)
    {
        Id = CopingSessionId.New();
        UserId = userId;
        CopingExerciseId = copingExerciseId;
        StartedAt = startedAt;
    }

    public static CopingSession Start(UserId userId, CopingExerciseId copingExerciseId, DateTimeOffset now) =>
        new(userId, copingExerciseId, now);

    public void Complete(DateTimeOffset completedAt)
    {
        CompletedAt = completedAt;
    }
}