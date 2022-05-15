using GameGenerator.Core.Abstractions.Services;
using GameGenerator.Core.Abstractions.Services.MapUsers;
using GameGenerator.Core.Services;
using GameGenerator.Core.Services.MapUsers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;


namespace GameGenerator.Core
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGameGeneratorServices(
            this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IConnectionService, ConnectionService>();
        }
    }
}
