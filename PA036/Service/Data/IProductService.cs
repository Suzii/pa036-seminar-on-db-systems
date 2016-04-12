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

        ProductDTO Get(int id, DbSettings dbSettings = null);

        Task<IList<ProductDTO>> GetAllAsync();

        IList<ProductDTO> GetAll();

        Task<IList<ProductDTO>> GetAsync(ProductFilter filter, DbSettings dbSettings = null);

        IList<ProductDTO> Get(ProductFilter filter, DbSettings dbSettings = null);

        Task<ProductDTO> CreateAsync(ProductDTO product);

        ProductDTO Create(ProductDTO product);

        Task<ProductDTO> UpdateAsync(ProductDTO product);

        ProductDTO Update(ProductDTO product);

        Task DeleteAsync(int id);

        void Delete(int id);

        Task<int> TotalCountAsync(DbSettings dbSettings = null);

        int TotalCount(DbSettings dbSettings = null);
    }
}
