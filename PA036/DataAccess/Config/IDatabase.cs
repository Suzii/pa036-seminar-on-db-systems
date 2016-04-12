
namespace DataAccess.Config
{
    public interface IDatabase
    {
        void InvalidateCache();

        int GetCacheItemsCount();
    }
}
