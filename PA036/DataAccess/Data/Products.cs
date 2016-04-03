using DataAccess.Context;
using DataAccess.Model;
using Shared.Modifiers;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Data
{
    public class Products : BaseData, IProducts
    {
        public IList<Product> Get(ProductModifier modifier = null)
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

        public int Count()
        {
            using (var db = new AppContext())
            {
                return db.Products.Count();
            }
        }

        public void Create(Product product)
        {
            using (var db = new AppContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new AppContext())
            {
                var product = db.Products.First(x => x.Id == id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (var db = new AppContext())
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
