using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealth.DataAccess
{
    public class DefaultRepository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class 
        where TKey : IEquatable<TKey>
    {
        protected readonly EHealthDbContext context;

        public DefaultRepository(EHealthDbContext context)
        {
            this.context = context;
        }

        public async Task Create(TEntity entity)
        {
            await context.AddAsync(entity);
        }

        public virtual async Task<TEntity> Get(TKey key)
        {
            return await context.FindAsync<TEntity>(key);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Task.FromResult(context.Set<TEntity>().AsEnumerable());
        }

        public async Task Remove(TKey key)
        {
            var entity = await Get(key); 
            context.Remove(entity);
        }

        public async Task Update(TKey key, Action<TEntity> setFields)
        {
            var entity = await Get(key);
            setFields(entity);
            context.Update(entity);
        }
    }
}
