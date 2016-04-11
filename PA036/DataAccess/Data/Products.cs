using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Products : BaseData, IProducts
    {
        public async Task<IList<Product>> GetAsync(ProductFilter filter = null, DbSettings dbSettings = null)
        {
            if (filter == null)
                filter = new ProductFilter();

            using (var db = CreateAppContext(dbSettings))
            {
                var products = db.Products.AsQueryable();
                if (filter.NameFilter != null)
                    products = products.Where(x => x.Name.Contains(filter.NameFilter));

                if (filter.UnitCostFilter.HasValue)
                    products = products.Where(x => x.UnitCost == filter.UnitCostFilter.Value);

                if (filter.StockCountFilter.HasValue)
                    products = products.Where(x => x.StockCount == filter.StockCountFilter.Value);

                products = ApplyBaseModifiers(products, filter);

                return await products.ToListAsync();
            }
        }

        public async Task<Product> GetAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await db.Products.FirstAsync(x => x.Id == id);
            }
        }

        public async Task<int> CountAsync(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await db.Products.CountAsync();
            }
        }

        public async Task CreateAsync(Product product, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var product = await db.Products.FirstAsync(x => x.Id == id);
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Product product, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var toUpdate = await db.Products.FirstAsync(x => x.Id == product.Id);
                toUpdate.Name = product.Name;
                toUpdate.StockCount = product.StockCount;
                toUpdate.UnitCost = product.UnitCost;
                await db.SaveChangesAsync();
            }
        }
    }
}
