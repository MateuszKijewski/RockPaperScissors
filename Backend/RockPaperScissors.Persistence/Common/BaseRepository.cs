using Dawn;
using Microsoft.EntityFrameworkCore;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Common;
using System.Linq.Expressions;

namespace RockPaperScissors.Persistence.Common
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : EntityBase, new()
    {
        private readonly RockPaperScissorsDbContext _dbContext;

        public BaseRepository(RockPaperScissorsDbContext dbContext)
        {
            _dbContext = Guard.Argument(dbContext, nameof(dbContext)).NotNull();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var result = new List<TEntity>(entities);

            await _dbContext.Set<TEntity>().AddRangeAsync(result);
            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params string[] expandProperties)
        {
            var dbSet = _dbContext.Set<TEntity>().Where(predicate);

            foreach (var expandProperty in expandProperties)
            {
                dbSet.Include(expandProperty).ToList();
            }

            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetAsync(Guid id, params string[] expandProperties)
        {
            var dbSet = _dbContext.Set<TEntity>();

            foreach (var expandProperty in expandProperties)
            {
                dbSet.Include(expandProperty).ToList();
            }

            var entity = await dbSet.FindAsync(id);

            if (entity == null)
                throw new Exception($"Entity with {id} doesnt exist");

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Guid[] ids)
        {
            var result = new List<TEntity>();
            TEntity? entity;
            foreach (var id in ids)
            {
                entity = await _dbContext.Set<TEntity>().FindAsync(id);
                if (entity == null)
                    throw new Exception($"Entity with {id} doesnt exist");

                result.Add(entity);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] expandProperties)
        {
            var dbSet = _dbContext.Set<TEntity>();

            foreach (var expandProperty in expandProperties)
            {
                dbSet.Include(expandProperty).ToList();
            }

            return await dbSet.ToListAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            try
            {
                var record = new TEntity() { Id = id };
                DetachTracked(id);
                _dbContext.Set<TEntity>().Remove(record);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Entity with {id} doesnt exist");
            }
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await RemoveAsync(entity.Id);
        }

        public async Task RemoveRangeAsync(Guid[] ids)
        {
            try
            {
                var records = ids.Select(x => new TEntity { Id = x });
                DetachTracked(ids);
                foreach (var record in records)
                {
                    _dbContext.Remove(record);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entity = ex.Entries.FirstOrDefault() as TEntity;
                if (entity == null)
                    throw;

                throw new Exception($"Entity with {entity.Id} doesnt exist");
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            await RemoveRangeAsync(entities.Select(x => x.Id).ToArray());
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Attach(entity);
                _dbContext.Set<TEntity>().Update(entity);
                await _dbContext.SaveChangesAsync();

                return entity;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new Exception($"Entity with {entity.Id} doesnt exist");
            }
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbContext.Set<TEntity>().AttachRange(entities);
                _dbContext.Set<TEntity>().UpdateRange(entities);
                await _dbContext.SaveChangesAsync();

                return entities;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entity = ex.Entries.FirstOrDefault() as TEntity;
                if (entity == null)
                    throw;

                throw new Exception($"Entity with {entity.Id} doesnt exist");
            }
        }

        private void DetachTracked(params Guid[] ids)
        {
            var tracked = _dbContext.Set<TEntity>().Local.Where(x => ids.Contains(x.Id));
            foreach (var trackedRecord in tracked)
            {
                _dbContext.Entry(trackedRecord).State = EntityState.Detached;
            }
        }
    }
}