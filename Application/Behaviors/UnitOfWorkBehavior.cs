using Application.Abstractions;
using MediatR;

namespace Application.Behaviors;

public sealed class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Let the handler run first
        var response = await next(cancellationToken);

        // Only commit for commands
        if (request is ICommand || IsGenericCommand(request))
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return response;
    }

    private static bool IsGenericCommand(TRequest request) =>
        request.GetType().GetInterfaces()
            .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>));
}