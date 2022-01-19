using Dawn;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Common;

namespace RockPaperScissors.Persistence.Common
{
    public class BaseRepositoryProvider : IBaseRepositoryProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public BaseRepositoryProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = Guard.Argument(serviceProvider, nameof(serviceProvider)).NotNull().Value;
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>()
            where TEntity : EntityBase
        {
            return _serviceProvider.GetRequiredService<IBaseRepository<TEntity>>();
        }
    }
}