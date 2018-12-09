using System.Linq;
using System.Threading.Tasks;

namespace Cmt.Dal.Interfaces.Repositories
{
    public interface IRepository<T, U>
    {
        IQueryable<T> GetAll();

        Task<T> GetAsync(U id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(U id);
    }
}
