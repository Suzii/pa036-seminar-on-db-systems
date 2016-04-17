using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;

namespace Service.TestScenarios
{
    public class Scenario2Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private readonly Scenario2Config _config;

        public Scenario2Service(Scenario2Config config)
        {
            _config = config;
            var dbSettings = new DbSettings() { UseSecondAppContext = config.UseRemoteDb };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return await GetsIncreasingCount();
        }

        /// <summary> 
        /// Simple GET query for k products executed by two users.
        /// 
        /// Number k is increased in every iteration
        /// Cache is invalidated before every get request of user A. 
        /// </summary>
        /// <returns>Measured data of both users</returns>
        private async Task<Scenario2Results> GetsIncreasingCount()
        {
            var totalCount = await _instance.TotalCountAsync();
            var modifier = new ProductFilter();

            var withoutCache = new List<double>();
            var withCache = new List<double>();
            var cacheSizes = new List<CacheSizeComparison>();

            const int step = 1000;
            _databaseService.InvalidateCache();


            for (var i = step; i <= totalCount; i += step)
            {
                if (_config.InvalidateCacheAfterIteration)
                {
                    _databaseService.InvalidateCache();
                }

                var cacheSize = new CacheSizeComparison();
                modifier.Take = i;
                cacheSize.NoOfObjectsReturnedInQuery = i;
                cacheSize.BeforeQueryExecution = _databaseService.GetCacheItemsCount();

                // GET 1
                var watch = Stopwatch.StartNew();
                await _instance.GetAsync(modifier);
                watch.Stop();

                withoutCache.Add(watch.ElapsedMilliseconds / 1000.0);
                cacheSize.AfterQueryExecution = _databaseService.GetCacheItemsCount();

                // GET 2
                watch.Restart();
                await _instance.GetAsync(modifier);
                watch.Stop();

                withCache.Add(watch.ElapsedMilliseconds / 1000.0);
                cacheSizes.Add(cacheSize);
            }

            var result = new Scenario2Results()
            {
                CachedQueriesTimes = withCache,
                NotCachedQueriesTimes = withoutCache,
                XAxis = new List<double>() { step, totalCount, step },
                CacheSizeComparison = cacheSizes
            };

            return result;
        }

    }
}