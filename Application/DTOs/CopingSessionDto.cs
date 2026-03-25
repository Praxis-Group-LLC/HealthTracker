using Domain.Coping;

namespace Application.DTOs;

public sealed record CopingSessionDto(
    CopingSessionId Id,
    CopingExerciseId CopingExerciseId,
    DateTimeOffset StartedAt,
    DateTimeOffset? CompletedAt)
{
    public static CopingSessionDto FromDomain(CopingSession session)
    {
        return new CopingSessionDto(session.Id, session.CopingExerciseId, session.StartedAt, session.CompletedAt);
    }
}