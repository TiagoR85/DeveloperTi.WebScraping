using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeveloperTi.WebScraping.Data
{
    public class RepositoryAsync<TEntity> : SpecificMethods<TEntity>, IRepositoryGeneric<TEntity> where TEntity : class, IIdentityEntity
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryAsync(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async virtual Task<TEntity> AddAsync(TEntity obj)
        {
            var r = await _dbSet.AddAsync(obj);
            return r.Entity;
        }

        public async virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet);
        }

        public async virtual Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async virtual Task<bool> RemoveAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            return RemoveAsync(entity);
        }

        public virtual bool RemoveAsync(TEntity obj)
        {
            return Task.Run(() => _dbSet.Remove(obj)).IsCompletedSuccessfully;
        }

        public virtual bool RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() => _dbSet.RemoveRange(entities)).IsCompletedSuccessfully;
        }

        public virtual void UpdateAsync(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
        }

        public virtual bool UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return Task.Run(() => _dbSet.UpdateRange(entities)).IsCompletedSuccessfully;
        }

        protected override IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, params string[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            query = GenerateQueryableWhereExpression(query, filter);
            query = GenerateIncludeProperties(query, includeProperties);

            if (order != null)
                return order(query);

            return query;
        }

        private IQueryable<TEntity> GenerateIncludeProperties(IQueryable<TEntity> query, string[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
            {
                query.Include(includeProperty);
            }
            return query;
        }

        private IQueryable<TEntity> GenerateQueryableWhereExpression(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
                query.Where(filter);
            return query;
        }

        protected override IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities, Func<TEntity, TEntity> DoAction)
        {
            foreach (var e in entities)
            {
                yield return DoAction(e);
            }
        }
    }
}
