using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class ScoresRepository : Repository<Scores>, IScoresRepository
    {
        private readonly BowlingDbContext _dbContext;

        public ScoresRepository(BowlingDbContext dbContext) : base(dbContext) 
            => _dbContext = dbContext;

        public async Task<IEnumerable<Scores>> GetByGameIdAsync(Guid gameId) 
            => await Task.FromResult(_dbContext.Scores.Where(x => x.GameId == gameId));
    }
}
