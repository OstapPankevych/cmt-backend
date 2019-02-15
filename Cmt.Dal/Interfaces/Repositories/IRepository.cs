using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cmt.Dal.Interfaces.Repositories
{
    public interface IRepository<TEntity, TId>
    {
        Task<IList<TEntity>> GetAsync();
        Task<TEntity> GetAsync(TId id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TId id);
    }
}
