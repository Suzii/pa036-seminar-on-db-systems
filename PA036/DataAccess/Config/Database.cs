using DataAccess.Context;
using Shared.Settings;

namespace DataAccess.Config
{
    public class Database : IDatabase
    {
        public void InvalidateCache()
        {
            if (Configuration.Cache == null)
                return;

            Configuration.Cache.InvalidateSets(new[] { nameof(Model.Product) });
        }

        public int GetCacheItemsCount()
        {
            if (Configuration.Cache == null)
                return 0;

            return Configuration.Cache.Count;
        }

        private AppContext CreateAppContext(DbSettings dbSettings)
        {
            if (dbSettings == null)
                dbSettings = new DbSettings();

            return dbSettings.UseSecondAppContext ? (AppContext)new AppContext2() : new AppContext1();
        }
    }
}