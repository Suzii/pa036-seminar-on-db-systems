using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Data
{
    public class ProductService: IProductService
    {
        private readonly IProducts<Product> _instance;

        public DbSettings DbSettings { get; set; }

        public ProductService(DbSettings dbSettings)
        {
            _instance = new Products();
            DbSettings = dbSettings;
        }


        public async Task<ProductDTO> GetAsync(int id)
        {
            return ToProductDto(await _instance.GetAsync(id, DbSettings));
        }

        public ProductDTO Get(int id)
        {
            return ToProductDto(_instance.Get(id, DbSettings));
        }

        public async Task<IList<ProductDTO>> GetAllAsync()
        {
            return await GetAsync(null);
        }

        public IList<ProductDTO> GetAll()
        {
            return Get(null);
        }

        public async Task<IList<ProductDTO>> GetAsync(ProductFilter filter)
        {
            return (await _instance.GetAsync(filter, DbSettings)).Select(ToProductDto).ToList();
        }

        public IList<ProductDTO> Get(ProductFilter filter)
        {
            return _instance.Get(filter, DbSettings).Select(ToProductDto).ToList();
        }

        // TODO ?? should return newly created product?
        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            await _instance.CreateAsync(product, DbSettings);
            return null; 
        }

        // TODO ?? should return newly created product?
        public ProductDTO Create(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Create(product, DbSettings);
            return null;
        }

        // TODO ?? should return updated product?
        public async Task<ProductDTO> UpdateAsync(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            await _instance.UpdateAsync(product, DbSettings);
            return null; 
        }

        // TODO ?? should return updated product?
        public ProductDTO Update(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Update(product, DbSettings);
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await _instance.DeleteAsync(id, DbSettings);
        }

        public void Delete(int id)
        {
            _instance.Delete(id, DbSettings);
        }

        public async Task<int> TotalCountAsync()
        {
            return await _instance.CountAsync(DbSettings);
        }

        public int TotalCount()
        {
            return _instance.Count(DbSettings);
        }

        private ProductDTO ToProductDto(Product product)
        {
            return (product != null)? new ProductDTO(product) : null;
        }

        private Product ToProduct(ProductDTO dto)
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