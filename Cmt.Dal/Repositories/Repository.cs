using System.Threading.Tasks;
using Cmt.Dal.Entities;
using Cmt.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Repositories
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

        public virtual async Task<U> CreateAsync(T entity)
        {
            DbContext.Add(entity);
            await DbContext.SaveChangesAsync();

            return entity.Id;
        }

        public virtual Task<T> GetAsync(U id)
        {
            return DbContext.FindAsync<T>(id);
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Remove(U id)
        {
            var entity = DbSet.Find(id);
            Remove(entity);
        }

        public virtual void Remove(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }
    }
}
