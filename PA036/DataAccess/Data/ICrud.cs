using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface ICrud<T1, T2> where T1 : IDataModel where T2 : BaseFilter
    {
        Task<IList<T1>> GetAsync(T2 filter = null, DbSettings dbSettings = null);

        IList<T1> Get(T2 filter = null, DbSettings dbSettings = null);

        Task<T1> GetAsync(int id, DbSettings dbSettings = null);

        T1 Get(int id, DbSettings dbSettings = null);

        Task<int> CountAsync(DbSettings dbSettings = null);

        int Count(DbSettings dbSettings = null);

        Task CreateAsync(T1 entity, DbSettings dbSettings = null);

        void Create(T1 entity, DbSettings dbSettings = null);

        Task DeleteAsync(int id, DbSettings dbSettings = null);

        void Delete(int id, DbSettings dbSettings = null);

        Task UpdateAsync(T1 entity, DbSettings dbSettings = null);

        void Update(T1 entity, DbSettings dbSettings = null);
    }
}
