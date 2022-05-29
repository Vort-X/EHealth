using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHealth.DataAccess
{
    public interface IRepository<TEntity, TKey> where TEntity : class where TKey : IEquatable<TKey>
    {
        Task Create(TEntity entity);
        Task<TEntity> Get(TKey key);
        Task<IEnumerable<TEntity>> GetAll();
        Task Remove(TKey key);
        Task Update(TKey key, Action<TEntity> setFields);
    }
}
