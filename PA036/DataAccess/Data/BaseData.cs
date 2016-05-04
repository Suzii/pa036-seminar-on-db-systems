using DataAccess.Context;
using DataAccess.LinqExtension;
using DataAccess.Model;
using Shared.Enums;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public abstract class BaseData<T1, T2> where T1 : class, IDataModel where T2 : BaseFilter
    {
        protected AppContext CreateAppContext(DbSettings dbSettings)
        {
            if (dbSettings == null)
                dbSettings = new DbSettings();

            switch(dbSettings.AppContext)
            {
                case AppContexts.Local:
                    return new AppContext1();
                case AppContexts.Azure:
                    return new AppContext2();
                case AppContexts.LocalNamedAsAzure:
                    return new AppContext3();
                default:
                    return new AppContext1();
            }
        }

        protected abstract DbSet<T1> GetDbSet(AppContext context);

        public IQueryable<T1> ApplyBaseModifiers(IQueryable<T1> query, T2 filter)
        {
            if (filter.Ids != null && filter.Ids.Any())
                query = query.Where(x => filter.Ids.Contains(x.Id));

            if (!string.IsNullOrWhiteSpace(filter.OrderProperty))
                query = query.OrderByField(filter.OrderProperty, filter.OrderDesc);

            if (filter.Skip > 0)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take > 0)
                query = query.Take(filter.Take.Value);

            return query;
        }

        public async Task<IList<T1>> GetAsync(T2 filter = null, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await GetQuery(db, filter).ToListAsync();
            }
        }

        public IList<T1> Get(T2 filter = null, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return GetQuery(db, filter).ToList();
            }
        }

        public async Task<T1> GetAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await GetDbSet(db).FirstAsync(x => x.Id == id);
            }
        }

        public T1 Get(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return GetDbSet(db).First(x => x.Id == id);
            }
        }

        public async Task<int> CountAsync(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await GetDbSet(db).CountAsync();
            }
        }

        public int Count(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return GetDbSet(db).Count();
            }
        }

        public async Task CreateAsync(T1 entity, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                GetDbSet(db).Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public void Create(T1 entity, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                GetDbSet(db).Add(entity);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var entity = await GetDbSet(db).FirstAsync(x => x.Id == id);
                GetDbSet(db).Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public void Delete(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var entity = GetDbSet(db).First(x => x.Id == id);
                GetDbSet(db).Remove(entity);
                db.SaveChanges();
            }
        }

        protected abstract IQueryable<T1> GetQuery(AppContext db, T2 filter);
    }
}
