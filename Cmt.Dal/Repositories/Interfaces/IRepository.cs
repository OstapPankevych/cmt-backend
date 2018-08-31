using System;
using System.Threading.Tasks;

namespace Cmt.Dal.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<int> CreateAsync(T entity);
        Task<T> GetAsync(int id);
    }
}
