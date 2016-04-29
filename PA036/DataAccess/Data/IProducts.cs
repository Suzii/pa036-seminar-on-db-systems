using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IProducts<T> where T : class
    {
        Task<IList<T>> GetAsync(ProductFilter filter = null, DbSettings dbSettings = null);

        IList<T> Get(ProductFilter filter = null, DbSettings dbSettings = null);

        Task<T> GetAsync(int id, DbSettings dbSettings = null);

        T Get(int id, DbSettings dbSettings = null);

        Task<int> CountAsync(DbSettings dbSettings = null);

        int Count(DbSettings dbSettings = null);

        Task CreateAsync(T product, DbSettings dbSettings = null);

        void Create(T product, DbSettings dbSettings = null);

        Task DeleteAsync(int id, DbSettings dbSettings = null);

        void Delete(int id, DbSettings dbSettings = null);

        Task UpdateAsync(T product, DbSettings dbSettings = null);

        void Update(T product, DbSettings dbSettings = null);
    }
}
