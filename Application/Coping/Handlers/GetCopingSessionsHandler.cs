using Application.Abstractions;
using Application.Coping.Queries;
using Application.DTOs;

namespace Application.Coping.Handlers;

public sealed class GetCopingSessionsHandler(ICopingSessionRepository copingSessions)
{
    public async Task<List<CopingSessionDto>> Handle(
        GetCopingSessionsQuery query,
        CancellationToken ct = default)
    {
        var list = await copingSessions.GetByUserAsync(query.UserId, query.To, query.From, ct);

        return list.Select(CopingSessionDto.FromDomain).ToList();
    }
}