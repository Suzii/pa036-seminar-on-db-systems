using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;
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

        public ProductDTO Get(int id, DbSettings dbSettings = null)
        {
            return ToProductDto(_instance.GetAsync(id, dbSettings).Result);
        }

        public IList<ProductDTO> GetAll()
        {
            return Get(null, null);
        }

        public IList<ProductDTO> Get(ProductFilter filter, DbSettings dbSettings = null)
        {
            return _instance.GetAsync(filter, dbSettings).Result.Select(ToProductDto).ToList();
        }

        // TODO ?? should return newly created product?
        public ProductDTO Create(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.CreateAsync(product).Wait();
            return null; 
        }

        // TODO ?? should return updated product?
        public ProductDTO Update(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.UpdateAsync(product).Wait();
            return null; 
        }

        public void Delete(int id)
        {
            _instance.DeleteAsync(id).Wait();
        }

        public int TotalCount()
        {
            return _instance.CountAsync().Result;
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