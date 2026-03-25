using Domain.Shared;

namespace Application.Coping.Queries;

public sealed record GetCopingSessionsQuery(UserId UserId, DateTimeOffset To, DateTimeOffset From);
