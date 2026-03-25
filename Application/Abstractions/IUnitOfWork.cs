namespace Application.Abstractions;

public interface IUnitOfWork
{
    /// <summary>
    /// Persist changes made in the current unit of work.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}