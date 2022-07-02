using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class GamesRepository : Repository<Games>, IGamesRepository
    {
        private readonly BowlingDbContext _dbContext;

        public GamesRepository(BowlingDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public async Task<Games> GetGamesByNameAsync(string name, CancellationToken cancellationToken)
            => await _dbContext.Games.FirstOrDefaultAsync(x => x.Name.Equals(name));
    }
}
