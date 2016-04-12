using DataAccess.Context;
using DataAccess.Model;
using Shared.Settings;
using System.Data.Entity.Migrations.History;

namespace DataAccess.Config
{
    public class Database : IDatabase
    {
        public void InvalidateCache()
        {
            if (Configuration.Cache == null)
                return;

            Configuration.Cache.InvalidateSets(new[] { nameof(HistoryRow), nameof(Product) });
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