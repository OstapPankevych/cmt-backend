using System.Linq;
using System.Threading.Tasks;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Ef.Repositories
{
    public abstract class Repository<T, U> : IRepository<T, U> where T: Entity<U>
    {
        protected CmtContext DbContext { get; }
        protected DbSet<T> DbSet;

        protected Repository(CmtContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public virtual async Task<T> GetAsync(U id)
        {
            return await DbContext.FindAsync<T>(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            DbSet.Add(entity);

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(U id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;

            await DbContext.SaveChangesAsync();
        }
    }
}
