using System.Collections.Generic;
using Service.DTO;
using Shared.Filters;

namespace Service.Data
{
    public interface IProductService
    {
        ProductDTO Get(int id);

        IList<ProductDTO> GetAll();

        IList<ProductDTO> Get(ProductFilter filter);

        ProductDTO Create(ProductDTO product);

        ProductDTO Update(ProductDTO product);

        void Delete(int id);

        int TotalCount();
    }
}
