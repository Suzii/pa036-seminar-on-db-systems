using DataAccess.Context;
using DataAccess.Model;
using DataAccess.Modifiers;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Data
{
    public class Products : BaseData
    {
        public static IList<Product> GetAllProducts(ProductModifier modifier = null)
        {
            if (modifier == null)
                modifier = new ProductModifier();

            using (var db = new AppContext())
            {
                var products = ApplyBaseModifiers(db.Products.AsQueryable(), modifier);

                return products.ToList();
            }
        }
    }
}
