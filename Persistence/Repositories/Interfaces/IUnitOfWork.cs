namespace Persistence.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGamesRepository Games { get; }
        IScoresRepository Scores { get; }

        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}
