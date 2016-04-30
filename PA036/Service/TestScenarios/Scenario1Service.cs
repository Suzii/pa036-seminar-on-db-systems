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
        private readonly Scenario1Config _config;

        public Scenario1Service(Scenario1Config config)
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
        /// test for parallel GET query from multiple users
        /// </summary>
        /// <returns>Measured data of all users</returns>
        private async Task<Scenario1Results> GetFromMoreUsers()
        {
            var modifier = new ProductFilter();
            var step = 1;
            var maxUsers = 100;

            var cacheQTime = new List<double>();
            var cacheSizes = new List<CacheSizeComparison>();

            _databaseService.InvalidateCache();
            modifier.Take = 100;

            List<Task> tasks = new List<Task>();
            for (var i = 0; i < maxUsers; i += step)
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
                    lock (cacheQTime) 
                    cacheQTime.Add(watch.ElapsedMilliseconds / 1000.0);
                    lock (cacheSizes) 
                    cacheSizes.Add(cacheSize);
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks.ToArray());


            var result = new Scenario1Results()
            {
                CachedQueriesTimes = cacheQTime,
                CacheSizeComparison = cacheSizes,
                XAxis = new List<double>() { step, maxUsers, step },
            };

            return result;
        }

    }
}