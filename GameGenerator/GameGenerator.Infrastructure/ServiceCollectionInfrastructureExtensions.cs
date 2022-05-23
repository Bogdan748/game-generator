using GameGenerator.Core.Abstractions.Repositories;
using GameGenerator.Core.Abstractions.Repositories.MapUsers;
using GameGenerator.Core.Abstractions.Repositories.OnGoingGame;
using GameGenerator.Infrastructure.Repositories;
using GameGenerator.Infrastructure.Repositories.MapUsers;
using GameGenerator.Infrastructure.Repositories.OnGoingGame;
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
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IConnectionRepository, ConnectionRepository>();

            services.AddTransient<IOnGoingGameRepository, OnGoingGameRepository>();
            services.AddTransient<IOnGoingCardsRepository, OnGoingCardsRepository>();

        }
    }
}
