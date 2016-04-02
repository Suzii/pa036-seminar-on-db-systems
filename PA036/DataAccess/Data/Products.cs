using DataAccess.Context;
using DataAccess.Model;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Data
{
    public class Products
    {
        public static IList<Product> GetAllProducts()
        {
            using (var db = new AppContext())
            {
                return db.Products.ToList();
            }
        }
    }
}
