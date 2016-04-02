using DataAccess.Context;
using DataAccess.Model;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Data
{
    public class Products : BaseData
    {
        public static IList<Product> GetProducts(ProductModifier modifier = null)
        {
            if (modifier == null)
                modifier = new ProductModifier();

            using (var db = new AppContext())
            {
                var products = db.Products.AsQueryable();
                if (modifier.NameFilter != null)
                    products = products.Where(x => x.Name.Contains(modifier.NameFilter));

                if (modifier.UnitCostFilter.HasValue)
                    products = products.Where(x => x.UnitCost == modifier.UnitCostFilter.Value);

                if (modifier.StockCountFilter.HasValue)
                    products = products.Where(x => x.StockCount == modifier.StockCountFilter.Value);

                products = ApplyBaseModifiers(products, modifier);

                return products.ToList();
            }
        }
    }
}
