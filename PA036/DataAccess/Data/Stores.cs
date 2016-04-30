using DataAccess.Context;
using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Stores : BaseData<Store, StoreFilter>, IStores
    {
        public async Task UpdateAsync(Store store, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var toUpdate = await db.Stores.FirstAsync(x => x.Id == store.Id);
                toUpdate.Name = store.Name;
                toUpdate.City = store.City;
                toUpdate.State = store.State;
                await db.SaveChangesAsync();
            }
        }

        public void Update(Store store, DbSettings dbSettings = null)
        {
            using (var db = CreateAppContext(dbSettings))
            {
                var toUpdate = db.Stores.First(x => x.Id == store.Id);
                toUpdate.Name = store.Name;
                toUpdate.City = store.City;
                toUpdate.State = store.State;
                db.SaveChanges();
            }
        }

        protected override IQueryable<Store> GetQuery(AppContext db, StoreFilter filter)
        {
            if (filter == null)
                filter = new StoreFilter();

            var stores = db.Stores.AsQueryable();
            if (filter.NameFilter != null)
                stores = stores.Where(x => x.Name.Contains(filter.NameFilter));

            if (filter.CityFilter != null)
                stores = stores.Where(x => x.City.Contains(filter.CityFilter));

            if (filter.StateFilter != null)
                stores = stores.Where(x => x.State.Contains(filter.StateFilter));

            return ApplyBaseModifiers(stores, filter);
        }

        protected override DbSet<Store> GetDbSet(AppContext context)
        {
            return context.Stores;
        }
    }
}
