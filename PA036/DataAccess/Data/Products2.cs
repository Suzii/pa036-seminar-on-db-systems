using DataAccess.Context;
using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Products2 : ProductsBase<Product2>
    {
        protected override DbSet<Product2> GetDbSet(AppContext appContext)
        {
            return appContext.Products2;
        }

        public override async Task UpdateAsync(Product2 product, DbSettings dbSettings = null)
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

        public override void Update(Product2 product, DbSettings dbSettings = null)
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

        protected override IQueryable<Product2> GetQuery(AppContext db, ProductFilter filter)
        {
            if (filter == null)
                filter = new ProductFilter();

            var products = db.Products2.AsQueryable();
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
