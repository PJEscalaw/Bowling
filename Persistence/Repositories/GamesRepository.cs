using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class GamesRepository : Repository<Games>, IGamesRepository
    {
        public GamesRepository(BowlingDbContext dbContext) :  base(dbContext) { }
    }
}
