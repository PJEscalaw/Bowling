using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Repositories.Interfaces;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<BowlingDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
