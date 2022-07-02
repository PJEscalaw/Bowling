using Domain.Entities;

namespace Persistence.Repositories.Interfaces
{
    public interface IGamesRepository : IRepository<Games>
    {
        Task<Games> GetGamesByNameAsync(string name, CancellationToken cancellationToken);
    }
}
