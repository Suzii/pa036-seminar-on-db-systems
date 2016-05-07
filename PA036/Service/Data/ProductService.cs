using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;

namespace Service.Data
{
    public class ProductService: BaseService<ProductDTO, Product, ProductFilter>, IProductService
    {
        public sealed override DbSettings DbSettings { get; set; }
        protected sealed override ICrud<Product, ProductFilter> DalInstance { get; set; }

        public ProductService(DbSettings dbSettings)
        {
            DalInstance = new Products();
            DbSettings = dbSettings;
        }
        
        protected override Product ToEntity(ProductDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                StockCount = dto.StockCount,
                UnitCost = dto.UnitCost,
                StoreId = dto.StoreId
            };

            return result;
        }

        protected override ProductDTO ToDto(Product entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = new ProductDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                StockCount = entity.StockCount,
                UnitCost = entity.UnitCost,
                StoreId = entity.StoreId
            };

            return result;
        }
    }
}