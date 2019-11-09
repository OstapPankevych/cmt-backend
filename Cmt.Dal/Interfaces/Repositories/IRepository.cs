using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cmt.Dal.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IList<TEntity>> GetAsync();
        Task<TEntity> GetAsync<TId>(TId id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity id);
    }
}
