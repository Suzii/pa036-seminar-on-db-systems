using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Service.Data
{
    public class ProductService
    {
        public static IList<ProductDTO> GetAllProducts()
        {
            return Products.GetAllProducts().Select(ToProductDTO).ToList();
        }

        private static ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO(product);
        }
    }
}