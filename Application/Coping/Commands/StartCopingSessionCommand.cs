using Domain.Coping;
using Domain.Shared;

namespace Application.Coping.Commands;

public sealed record StartCopingSessionCommand(
    UserId UserId,
    CopingExerciseId CopingExerciseId,
    DateTimeOffset StartedAt);