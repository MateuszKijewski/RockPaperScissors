using System.Linq.Expressions;

namespace RockPaperScissors.Application.Common.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> GetAsync(Guid id, params string[] expandProperties);

        Task<IEnumerable<TEntity>> GetAsync(Guid[] ids);

        Task<IEnumerable<TEntity>> GetAllAsync(params string[] expandProperties);

        Task<TEntity> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task RemoveAsync(Guid id);

        Task RemoveAsync(TEntity entity);

        Task RemoveRangeAsync(Guid[] ids);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] expandProperties);
    }
}