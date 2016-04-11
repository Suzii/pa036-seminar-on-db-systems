using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IProducts
    {
        Task<IList<Product>> GetAsync(ProductFilter filter = null, DbSettings dbSettings = null);

        Task<Product> GetAsync(int id, DbSettings dbSettings = null);

        Task<int> CountAsync(DbSettings dbSettings = null);

        Task CreateAsync(Product product, DbSettings dbSettings = null);

        Task DeleteAsync(int id, DbSettings dbSettings = null);

        Task UpdateAsync(Product product, DbSettings dbSettings = null);
    }
}
