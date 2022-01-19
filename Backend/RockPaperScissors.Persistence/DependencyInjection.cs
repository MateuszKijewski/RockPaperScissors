using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Persistence.Common;

namespace RockPaperScissors.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RockPaperScissorsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RockPaperScissorsDbContextDatabase")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBaseRepositoryProvider, BaseRepositoryProvider>();

            return services;
        }
    }
}