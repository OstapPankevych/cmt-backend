using System.Threading.Tasks;
using Cmt.Dal.Entities;
using Cmt.Dal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cmt.Dal.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T: Entity
    {
        protected DbContextOptions<CmtContext> options;

        protected Repository(DbContextOptions<CmtContext> options)
        {
            this.options = options;
        }

        public async Task<int> CreateAsync(T entity)
        {
            using (var db = CreateContext())
            {
                db.Add(entity);
                await db.SaveChangesAsync();

                return entity.Id;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (var db = CreateContext())
            {
                return await db.FindAsync<T>(id);
            }
        }

        public async void UpdateAsync(T entity)
        {
            using (var db = CreateContext())
            {
                db.Update(entity);
                await db.SaveChangesAsync();
            }
        }

        public async void RemoveAsync(T entity)
        {
            using (var db = CreateContext())
            {
                db.Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        protected CmtContext CreateContext()
        {
            return new CmtContext(options);
        }
    }
}
