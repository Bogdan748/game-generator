using GameGenerator.Core.Abstractions.Repositories;
using GameGenerator.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace GameGenerator.Infrastructure
{
    public static class ServiceCollectionInfrastructureExtensions
    {
        public static void AddGameGeneratorRepositories(
                this IServiceCollection services)
        {
            services.AddTransient<ICardRepository, CardRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
        }
    }
}
