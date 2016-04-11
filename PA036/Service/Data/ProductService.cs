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

        public ProductService()
        {
            _instance = new Products();
        }

        public async Task<ProductDTO> GetAsync(int id, DbSettings dbSettings = null)
        {
            return ToProductDto(await _instance.GetAsync(id, dbSettings));
        }

        public ProductDTO Get(int id, DbSettings dbSettings = null)
        {
            return ToProductDto(_instance.Get(id, dbSettings));
        }

        public async Task<IList<ProductDTO>> GetAllAsync()
        {
            return await GetAsync(null, null);
        }

        public IList<ProductDTO> GetAll()
        {
            return Get(null, null);
        }

        public async Task<IList<ProductDTO>> GetAsync(ProductFilter filter, DbSettings dbSettings = null)
        {
            return (await _instance.GetAsync(filter, dbSettings)).Select(ToProductDto).ToList();
        }

        public IList<ProductDTO> Get(ProductFilter filter, DbSettings dbSettings = null)
        {
            return _instance.Get(filter, dbSettings).Select(ToProductDto).ToList();
        }

        // TODO ?? should return newly created product?
        public async Task<ProductDTO> CreateAsync(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            await _instance.CreateAsync(product);
            return null; 
        }

        // TODO ?? should return newly created product?
        public ProductDTO Create(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Create(product);
            return null;
        }

        // TODO ?? should return updated product?
        public async Task<ProductDTO> UpdateAsync(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            await _instance.UpdateAsync(product);
            return null; 
        }

        // TODO ?? should return updated product?
        public ProductDTO Update(ProductDTO productDto)
        {
            var product = ToProduct(productDto);
            _instance.Update(product);
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await _instance.DeleteAsync(id);
        }

        public void Delete(int id)
        {
            _instance.Delete(id);
        }

        public async Task<int> TotalCountAsync()
        {
            return await _instance.CountAsync();
        }

        public int TotalCount()
        {
            return _instance.Count();
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