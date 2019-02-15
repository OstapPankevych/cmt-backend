using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IService<TDto, TId>
    {
        Task<TId> CreateAsync(TDto entity);

        Task<TDto> GetAsync(TId id);

        Task<IList<TDto>> GetAsync();

        Task UpdateAsync(TDto dto);

        Task DeleteAsync(TId id);
    }
}
