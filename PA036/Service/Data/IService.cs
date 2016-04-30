using System.Collections.Generic;
using System.Threading.Tasks;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;

namespace Service.Data
{
    public interface IService<TDto, in TDtoFilter> where TDto: IDTO where TDtoFilter: BaseFilter
    {
        DbSettings DbSettings { get; set; }

        Task<TDto> GetAsync(int id);

        TDto Get(int id);

        Task<IList<TDto>> GetAllAsync();

        IList<TDto> GetAll();

        Task<IList<TDto>> GetAsync(TDtoFilter filter);

        IList<TDto> Get(TDtoFilter filter);

        Task<TDto> CreateAsync(TDto dto);

        TDto Create(TDto dto);

        Task<TDto> UpdateAsync(TDto dto);

        TDto Update(TDto dto);

        Task DeleteAsync(int id);

        void Delete(int id);

        Task<int> TotalCountAsync();

        int TotalCount();
    }
}
