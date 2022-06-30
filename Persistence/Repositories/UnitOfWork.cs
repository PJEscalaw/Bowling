using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool IsDisposed;
        private readonly BowlingDbContext _dbContext;

        public IGamesRepository Games => new GamesRepository(_dbContext);
        public IScoresRepository Scores => new ScoresRepository(_dbContext);

        public UnitOfWork(BowlingDbContext dbContext) => _dbContext = dbContext;
        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();
        public async Task RollbackAsync() => await _dbContext.DisposeAsync();
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;
            if (disposing) _dbContext.Dispose();

            IsDisposed = true;
        }

        ~UnitOfWork() => Dispose(false);
    }
}
