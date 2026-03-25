using Application.Abstractions;
using Application.Coping.Commands;

namespace Application.Coping.Handlers;

public sealed class CompleteCopingSessionHandler(ICopingSessionRepository sessions)
{
    public async Task Handle(CompleteCopingSessionCommand command, CancellationToken ct = default)
    {
        var session = await sessions.GetByIdAsync(command.CopingSessionId, ct)
                      ?? throw new InvalidOperationException("Coping session not found.");

        session.Complete(command.CompletedAt);

        // If your repository pattern does tracking, this is enough;
        // otherwise you can add an explicit UpdateAsync if desired.
        // await _sessions.UpdateAsync(session, ct);
    }
}