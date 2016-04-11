using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;

namespace DataAccess.Data
{
    public interface IProducts
    {
        IList<Product> Get(ProductFilter filter = null, DbSettings dbSettings = null);

        Product Get(int id, DbSettings dbSettings = null);

        int Count(DbSettings dbSettings = null);

        void Create(Product product, DbSettings dbSettings = null);

        void Delete(int id, DbSettings dbSettings = null);

        void Update(Product product, DbSettings dbSettings = null);
    }
}
