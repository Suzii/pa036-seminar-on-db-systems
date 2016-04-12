
namespace Service.Config
{
    public interface IDatabaseService
    {
        void InvalidateCache();

        int GetCacheItemsCount();
    }
}
