using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef.Repositories
{
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity: Entity<TId>
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

        public virtual async Task<TEntity> GetAsync(TId id)
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

        public virtual async Task RemoveAsync(TId id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;

            await DbContext.SaveChangesAsync();
        }
    }
}
