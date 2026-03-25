using Domain.Coping;

namespace Application.DTOs;

public sealed record StartCopingSessionRequest(
    CopingExerciseId ExerciseId,
    DateTimeOffset? StartedAtUtc);