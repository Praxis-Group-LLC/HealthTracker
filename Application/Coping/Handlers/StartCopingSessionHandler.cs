using Application.Abstractions;
using Application.Coping.Commands;
using Domain.Coping;

namespace Application.Coping.Handlers;

public sealed class StartCopingSessionHandler(
    ICopingExerciseRepository exercises,
    ICopingSessionRepository sessions)
{
    public async Task<CopingSessionId> Handle(StartCopingSessionCommand command, CancellationToken ct = default)
    {
        var exercise = await exercises.GetByIdAsync(command.CopingExerciseId, ct)
                       ?? throw new InvalidOperationException("Coping exercise not found.");

        var session = CopingSession.Start(command.UserId, exercise.Id, command.StartedAt);
        await sessions.AddAsync(session, ct);

        return session.Id;
    }
}