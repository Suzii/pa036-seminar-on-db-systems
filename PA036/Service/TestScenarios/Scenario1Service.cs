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
    public class Scenario1Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private readonly Scenario2Config _config;

        public Scenario1Service(Scenario2Config config)
        {
            _config = config;
            var dbSettings = new DbSettings() { UseSecondAppContext = config.UseRemoteDb };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return await GetFromMoreUsers();
        }

        /// <summary> 
        /// Simple GET query for k products executed by two users.
        /// 
        /// Number k is increased in every iteration
        /// Cache is invalidated before every get request of user A. 
        /// </summary>
        /// <returns>Measured data of both users</returns>
        private async Task<Scenario1Results> GetFromMoreUsers()
        {
            var modifier = new ProductFilter();
            var step = 1;
            var totalCount = 100;

            var withoutCache = new List<double>();
            var withCache = new List<double>();
            var cacheSizes = new List<CacheSizeComparison>();

            _databaseService.InvalidateCache();
            modifier.Take = 100;

            // GET 1
            List<Task> tasks = new List<Task>();
            for (var i = 0; i < totalCount; i++)
            {

                Task task = Task.Run(async () =>
                {
                    var cacheSize = new CacheSizeComparison();
                    cacheSize.BeforeQueryExecution = _databaseService.GetCacheItemsCount();
                    var watch = Stopwatch.StartNew();
                    
                    await _instance.GetAsync(modifier);
                    watch.Stop();
                    cacheSize.NoOfObjectsReturnedInQuery = 100;
                    cacheSize.AfterQueryExecution = _databaseService.GetCacheItemsCount();
                    lock (withCache) 
                    withCache.Add(watch.ElapsedMilliseconds / 1000.0);
                    lock (cacheSizes) 
                    cacheSizes.Add(cacheSize);
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());


            var result = new Scenario1Results()
            {
                CachedQueriesTimes = withCache,
                CacheSizeComparison = cacheSizes,
                XAxis = new List<double>() { step, totalCount, step },
            };

            return result;
        }

    }
}