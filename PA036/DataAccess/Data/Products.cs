﻿using System.Data.Entity;
using DataAccess.Context;
using DataAccess.Model;
using System.Threading.Tasks;
using Shared.Settings;
using System.Linq;
using Shared.Filters;

namespace DataAccess.Data
{
    public class Products : ProductsBase<Product>
    {
        protected override DbSet<Product> GetDbSet(AppContext appContext)
        {
            return appContext.Products;
        }

        public override async Task UpdateAsync(Product product, DbSettings dbSettings = null)
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

        public override void Update(Product product, DbSettings dbSettings = null)
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

        protected override IQueryable<Product> GetQuery(AppContext db, ProductFilter filter)
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
