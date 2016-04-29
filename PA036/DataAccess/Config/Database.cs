using DataAccess.Model;
using System.Data.Entity.Migrations.History;

namespace DataAccess.Config
{
    public class Database : IDatabase
    {
        public void InvalidateCache()
        {
            if (Configuration.Cache == null)
                return;

            Configuration.Cache.InvalidateSets(new[] { nameof(HistoryRow), nameof(Product), nameof(Product2) });
        }

        public int GetCacheItemsCount()
        {
            if (Configuration.Cache == null)
                return 0;

            return Configuration.Cache.Count;
        }
    }
}
