using System.Collections.Generic;
using System.Threading.Tasks;
using Cmt.Bll.DTOs;

namespace Cmt.Bll.Services.Interfaces
{
    public interface IService<TDto, TId>
        where TDto: Dto<TId>
    {
        Task<TDto> CreateAsync(TDto dto);

        Task<TDto> GetAsync(TId id);

        Task<IList<TDto>> GetAsync();

        Task UpdateAsync(TDto dto);

        Task DeleteAsync(TDto dto);
    }
}
