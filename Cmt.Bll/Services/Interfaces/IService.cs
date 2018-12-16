using System;
using System.Threading.Tasks;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IService<TDto, TId>
    {
        Task<TId> CreateAsync(TDto entity);

        Task<TDto> GetAsync(TId id);

        Task UpdateAsync(TDto dto, DateTime lastModified);

        Task DeleteAsync(TId id);
    }
}
