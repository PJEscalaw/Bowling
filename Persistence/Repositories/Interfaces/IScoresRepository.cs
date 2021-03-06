using Domain.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IScoresRepository : IRepository<Scores>
    {
        Task<IEnumerable<Scores>> GetByGameIdAsync(Guid gameId);
    }
}
