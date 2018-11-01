using System.Threading.Tasks;

namespace Cmt.Dal.Repositories.Interfaces
{
    public interface IRepository<T, U>
    {
        Task<U> CreateAsync(T entity);
        Task<T> GetAsync(U id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(U id);
    }
}
