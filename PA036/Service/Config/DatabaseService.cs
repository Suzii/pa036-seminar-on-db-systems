using DataAccess.Config;
using Shared.Settings;

namespace Service.Config
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IDatabase _instance;

        public DatabaseService()
        {
            _instance = new Database();
        }

        public void InvalidateCache()
        {
            _instance.InvalidateCache();
        }

        public int GetCacheItemsCount()
        {
            return _instance.GetCacheItemsCount();
        }
    }
}