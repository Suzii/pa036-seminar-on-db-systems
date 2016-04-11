using DataAccess.Context;
using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Data
{
    public class Products : BaseData, IProducts
    {
        public IList<Product> Get(ProductFilter filter = null, DbSettings dbSettings = null)
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

                return products.ToList();
            }
        }

        public Product Get(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return db.Products.First(x => x.Id == id);
            }
        }

        public int Count(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return db.Products.Count();
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

        public void Delete(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var product = db.Products.First(x => x.Id == id);
                db.Products.Remove(product);
                db.SaveChanges();
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
    }
}
