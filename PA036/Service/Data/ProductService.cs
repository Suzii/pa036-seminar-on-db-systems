using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Linq;

namespace Service.Data
{
    public class ProductService
    {
        public static IList<ProductDTO> GetAllProducts()
        {
            return Products.GetProducts().Select(ToProductDTO).ToList();
        }

        public static IList<ProductDTO> GetProducts(ProductModifier modifier)
        {
            return Products.GetProducts(modifier).Select(ToProductDTO).ToList();
        }

        private static ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO(product);
        }
    }
}