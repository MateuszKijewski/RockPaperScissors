using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Application.Auth.Providers;
using RockPaperScissors.Application.Auth.Services;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Application.Sercurity.Services;

namespace RockPaperScissors.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}