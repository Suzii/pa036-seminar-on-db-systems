using System.Collections.Generic;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;

namespace Service.Data
{
    public interface IProductService
    {
        ProductDTO Get(int id, DbSettings dbSettings = null);

        IList<ProductDTO> GetAll();

        IList<ProductDTO> Get(ProductFilter filter, DbSettings dbSettings = null);

        ProductDTO Create(ProductDTO product);

        ProductDTO Update(ProductDTO product);

        void Delete(int id);

        int TotalCount();
    }
}
