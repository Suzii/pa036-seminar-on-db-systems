using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Service.Data
{
    public class ProductService: IProductService
    {
        private readonly IProducts _instance;

        public ProductService()
        {
            _instance = new Products();
        }

        public ProductDTO Get(int id)
        {
            return ToProductDto(_instance.Get(id));
        }

        public IList<ProductDTO> GetAll()
        {
            return Get(null);
        }

        public IList<ProductDTO> Get(ProductFilter filter)
        {
            return _instance.Get(filter).Select(ToProductDto).ToList();
        }

        // TODO ?? should return newly created product?
        public ProductDTO Create(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Create(product);
            return null; 
        }

        // TODO ?? should return updated product?
        public ProductDTO Update(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Update(product);
            return null; 
        }

        public void Delete(int id)
        {
            _instance.Delete(id);
        }

        public int TotalCount()
        {
            return _instance.Count();
        }

        private static ProductDTO ToProductDto(Product product)
        {
            return (product != null)? new ProductDTO(product) : null;
        }

        private static Product ToProduct(ProductDTO dto)
        {
            var result = new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                StockCount = dto.StockCount,
                UnitCost = dto.UnitCost
            };

            return result;
        }
    }
}