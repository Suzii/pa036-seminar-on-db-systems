using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;
using Shared.Enums;

namespace Service.TestScenarios
{
    public class Scenario1Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private readonly Scenario1Config _config;

        public Scenario1Service(Scenario1Config config)
        {
            _config = config;
            var dbSettings = new DbSettings { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return await GetFromMoreUsers();
        }

        /// <summary> 
        /// Test for parallel GET query from multiple users
        /// GET query on 100 same products performed in parallel by 100 threads (users)
        /// 
        /// </summary>
        /// <returns>Measures time it takes to for each query and number of items in cache for each thread execution</returns>
        private async Task<Scenario1Results> GetFromMoreUsers()
        {
            var modifier = new ProductFilter
            {
                Take = 100
            };

            var step = 1;
            var maxUsers = 100;

            var cacheQTime = new List<double>();
            var cacheSizes = new List<CacheSizeComparison>();

            _databaseService.InvalidateCache();

            List<Task> tasks = new List<Task>();
            for (var i = 0; i < maxUsers; i += step)
            {

                Task task = Task.Run(async () =>
                {
                    var cacheSize = new CacheSizeComparison
                    {
                        BeforeQueryExecution = _databaseService.GetCacheItemsCount()
                    };

                    var watch = Stopwatch.StartNew();
                    await _instance.GetAsync(modifier);
                    watch.Stop();

                    cacheSize.NoOfObjectsReturnedInQuery = 100;
                    cacheSize.AfterQueryExecution = _databaseService.GetCacheItemsCount();
                    lock (cacheQTime) 
                    cacheQTime.Add(watch.ElapsedMilliseconds / 1000.0);
                    lock (cacheSizes) 
                    cacheSizes.Add(cacheSize);
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks.ToArray());

            var result = new Scenario1Results
            {
                CachedQueriesTimes = cacheQTime,
                CacheSizeComparison = cacheSizes,
                XAxis = new List<double> { step, maxUsers, step },
            };

            return result;
        }

    }
}