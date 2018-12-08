using System;
using System.Threading.Tasks;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IService<T, U>
    {
        Task<U> CreateAsync(T dto);

        Task<T> GetAsync(U id);

        Task UpdateAsync(
            T dto,
            int updatedBy,
            DateTime unmodifiedSince);

        Task DeleteAsync(U id);
    }
}
