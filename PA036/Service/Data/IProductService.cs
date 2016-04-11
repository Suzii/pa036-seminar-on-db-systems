using System.Collections.Generic;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;
using System.Threading.Tasks;

namespace Service.Data
{
    public interface IProductService
    {
        Task<ProductDTO> GetAsync(int id, DbSettings dbSettings = null);

        Task<IList<ProductDTO>> GetAllAsync();

        Task<IList<ProductDTO>> GetAsync(ProductFilter filter, DbSettings dbSettings = null);

        Task<ProductDTO> CreateAsync(ProductDTO product);

        Task<ProductDTO> UpdateAsync(ProductDTO product);

        Task DeleteAsync(int id);

        Task<int> TotalCountAsync();
    }
}
