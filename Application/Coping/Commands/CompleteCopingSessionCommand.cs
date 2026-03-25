using Domain.Coping;

namespace Application.Coping.Commands;

public sealed record CompleteCopingSessionCommand(
    CopingSessionId CopingSessionId,
    DateTimeOffset CompletedAt);