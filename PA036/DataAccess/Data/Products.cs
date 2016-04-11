using DataAccess.Context;
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
            using (var db = CreateAppContext(dbSettings))
            {
                return await GetQuery(db, filter).ToListAsync();
            }
        }

        public IList<Product> Get(ProductFilter filter = null, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return GetQuery(db, filter).ToList();
            }
        }

        public async Task<Product> GetAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await db.Products.FirstAsync(x => x.Id == id);
            }
        }

        public Product Get(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return db.Products.First(x => x.Id == id);
            }
        }

        public async Task<int> CountAsync(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await db.Products.CountAsync();
            }
        }

        public int Count(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return db.Products.Count();
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

        public void Create(Product product, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                db.Products.Add(product);
                db.SaveChanges();
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

        public void Delete(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var product = db.Products.First(x => x.Id == id);
                db.Products.Remove(product);
                db.SaveChanges();
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

        public void Update(Product product, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var toUpdate = db.Products.First(x => x.Id == product.Id);
                toUpdate.Name = product.Name;
                toUpdate.StockCount = product.StockCount;
                toUpdate.UnitCost = product.UnitCost;
                db.SaveChanges();
            }
        }

        private IQueryable<Product> GetQuery(AppContext db, ProductFilter filter)
        {
            if (filter == null)
                filter = new ProductFilter();

            var products = db.Products.AsQueryable();
            if (filter.NameFilter != null)
                products = products.Where(x => x.Name.Contains(filter.NameFilter));

            if (filter.UnitCostFilter.HasValue)
                products = products.Where(x => x.UnitCost == filter.UnitCostFilter.Value);

            if (filter.StockCountFilter.HasValue)
                products = products.Where(x => x.StockCount == filter.StockCountFilter.Value);

            return ApplyBaseModifiers(products, filter);
        }
    }
}
