using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeveloperTi.WebScraping.Data
{
    public interface IRepositoryGeneric<TEntity> where TEntity : class, IIdentityEntity
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        void UpdateAsync(TEntity obj);
        bool UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> RemoveAsync(object id);
        bool RemoveAsync(TEntity obj);
        bool RemoveRangeAsync(IEnumerable<TEntity> entities);
    }
}