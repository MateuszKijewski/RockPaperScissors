using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Domain.Games;

namespace RockPaperScissors.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IGameSessionService, GameSessionService>();

            return services;
        }
    }
}