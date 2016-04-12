using EFCache;
using System.Data.Entity;
using System.Data.Entity.Core.Common;

namespace DataAccess.Config
{
    public class Configuration : DbConfiguration
    {
        public static InMemoryCache Cache { get; private set; }

        public Configuration()
        {
            Cache = new InMemoryCache();
            var transactionHandler = new CacheTransactionHandler(Cache);

            AddInterceptor(transactionHandler);

            var cachingPolicy = new CachingPolicy();

            Loaded +=
              (sender, args) => args.ReplaceService<DbProviderServices>(
                (s, _) => new CachingProviderServices(s, transactionHandler,
                  cachingPolicy));
        }
    }
}
