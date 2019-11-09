using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cmt.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        protected CmtContext DbContext { get; }
        protected DbSet<TEntity> DbSet;

        protected Repository(CmtContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        public async Task<IList<TEntity>> GetAsync()
        {
            return await GetAll().ToArrayAsync();
        }

        public virtual async Task<TEntity> GetAsync<TId>(TId id)
        {
            return await DbContext.FindAsync<TEntity>(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbContext.Update(entity);

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            DbContext.Remove(entity);

            await DbContext.SaveChangesAsync();
        }
    }
}
