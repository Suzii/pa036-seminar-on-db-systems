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
        private readonly IProducts _instance;
        private readonly DbSettings _dbSettings;

        public ProductService(DbSettings dbSettings)
        {
            this._instance = new Products();
            this._dbSettings = dbSettings;
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            return ToProductDto(await _instance.GetAsync(id, _dbSettings));
        }

        public ProductDTO Get(int id)
        {
            return ToProductDto(_instance.Get(id, _dbSettings));
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
            return (await _instance.GetAsync(filter, _dbSettings)).Select(ToProductDto).ToList();
        }

        public IList<ProductDTO> Get(ProductFilter filter)
        {
            return _instance.Get(filter, _dbSettings).Select(ToProductDto).ToList();
        }

        // TODO ?? should return newly created product?
        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            await _instance.CreateAsync(product, _dbSettings);
            return null; 
        }

        // TODO ?? should return newly created product?
        public ProductDTO Create(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Create(product, _dbSettings);
            return null;
        }

        // TODO ?? should return updated product?
        public async Task<ProductDTO> UpdateAsync(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            await _instance.UpdateAsync(product, _dbSettings);
            return null; 
        }

        // TODO ?? should return updated product?
        public ProductDTO Update(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Update(product, _dbSettings);
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await _instance.DeleteAsync(id, _dbSettings);
        }

        public void Delete(int id)
        {
            _instance.Delete(id, _dbSettings);
        }

        public async Task<int> TotalCountAsync()
        {
            return await _instance.CountAsync(_dbSettings);
        }

        public int TotalCount()
        {
            return _instance.Count(_dbSettings);
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