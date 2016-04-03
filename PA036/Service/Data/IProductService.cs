using System.Collections.Generic;
using System.Dynamic;
using Service.DTO;
using Shared.Modifiers;

namespace Service.Data
{
    public interface IProductService
    {
        IList<ProductDTO> GetAll();

        IList<ProductDTO> Get(ProductModifier modifier);

        ProductDTO Create(ProductDTO product);

        ProductDTO Update(int id, ProductDTO product);

        void Delete(int id);
    }
}
