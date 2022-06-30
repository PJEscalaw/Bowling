using Facade.Services.Games;
using Facade.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Facade
{
    public static class ServiceRegistration
    {
        public static void AddFacade(this IServiceCollection services)
        {
            services.AddScoped<IGamesService, GamesService>();
        }
    }
}
