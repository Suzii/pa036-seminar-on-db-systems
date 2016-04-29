using DataAccess.Context;
using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public abstract class ProductsBase<T> : BaseData, IProducts<T> where T : class, IDataModel
    {
        protected abstract DbSet<T> GetDbSet(AppContext appContext);

        public async Task<IList<T>> GetAsync(ProductFilter filter = null, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await GetQuery(db, filter).ToListAsync();
            }
        }

        public IList<T> Get(ProductFilter filter = null, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return GetQuery(db, filter).ToList();
            }
        }

        public async Task<T> GetAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return await GetDbSet(db).FirstAsync(x => x.Id == id);
            }
        }

        public T Get(int id, DbSettings dbSettings = null)
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
                return await db.Products.CountAsync();
            }
        }

        public int Count(DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                return db.Products.Count();
            }
        }

        public async Task CreateAsync(T product, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                GetDbSet(db).Add(product);
                await db.SaveChangesAsync();
            }
        }

        public void Create(T product, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                GetDbSet(db).Add(product);
                db.SaveChanges();
            }
        }

        public async Task DeleteAsync(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var product = await db.Products.FirstAsync(x => x.Id == id);
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public void Delete(int id, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var product = db.Products.First(x => x.Id == id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        protected abstract IQueryable<T> GetQuery(AppContext db, ProductFilter filter);
        public abstract Task UpdateAsync(T product, DbSettings dbSettings = null);
        public abstract void Update(T product, DbSettings dbSettings = null);
    }
}
