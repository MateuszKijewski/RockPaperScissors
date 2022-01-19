using RockPaperScissors.Domain.Common;

namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface IBaseRepositoryProvider
    {
        IBaseRepository<TEntity> GetRepository<TEntity>()
            where TEntity : EntityBase;
    }
}