using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class ScoresRepository : Repository<Scores>, IScoresRepository
    {
        public ScoresRepository(BowlingDbContext dbContext) : base(dbContext) {}
    }
}
