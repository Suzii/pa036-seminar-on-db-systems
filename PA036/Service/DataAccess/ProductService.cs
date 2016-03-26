using DataAccess.AppContext;
using DataAccess.Model;
using Service.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Service.DataAccess
{
    public class ProductService
    {
        public static List<ProductDTO> GetAllProducts()
        {
            using (var db = new AppContext())
            {
                return db.Products.Select(ToProductDTO).ToList();
            }
        }

        private static ProductDTO ToProductDTO(Product product)
        {
            return new ProductDTO(product);
        }
    }
}