using GameGenerator.Core.Abstractions.Services;
using GameGenerator.Core.Services;
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
        }
    }
}
