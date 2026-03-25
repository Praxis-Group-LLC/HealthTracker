namespace Application.DTOs;

public sealed record CompleteCopingSessionRequest(
    DateTimeOffset? CompletedAtUtc);