using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;
using System;

namespace Service.TestScenarios
{
    public class Scenario5Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private readonly Scenario1Config _config;

        public Scenario5Service(Scenario1Config config)
        {
            _config = config;
            var dbSettings = new DbSettings() { UseSecondAppContext = config.UseRemoteDb };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
            var productFilter = new ProductFilter();
            productFilter.Take = 1;
            _instance.Get(productFilter);
            _instance.Get(productFilter);
            _databaseService.InvalidateCache();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return  await getOverlappedData();
        }

        /// <summary> 
        /// Simple overlapping GET queries.
        /// 
        /// Anmount of overlaping items is increasing
        /// </summary>
        /// <returns>Measured data of test</returns>
        private async Task<Scenario5Results> getOverlappedData()
        {
            var modifier = new ProductFilter();
            modifier.OrderDesc = true;
            modifier.OrderProperty = "id";
            var amountOfData = 200;
            var originalCache = new List<double>();
            var overlappedCache = new List<double>();

            var overlapping = new List<List<int>>();
            var cacheSizes = new List<CacheSizeComparison>();

            _databaseService.InvalidateCache();

            var percentage = 0.7;
            for (var i = percentage; i >= 0; i -= 0.3)
            {
                _databaseService.InvalidateCache();
                var dataIteration = new List<int>();
                dataIteration.Add(100 - (int)(i * 100));

                var cacheSize = new CacheSizeComparison();
                cacheSize.NoOfObjectsReturnedInQuery = amountOfData;
                modifier.Take = amountOfData;
                modifier.Skip = 0;

                // GET 1
                var watch = Stopwatch.StartNew();
                await _instance.GetAsync(modifier);
                watch.Stop();
                originalCache.Add(watch.ElapsedMilliseconds / 1000.0);
                
                cacheSize.BeforeQueryExecution = _databaseService.GetCacheItemsCount();

                // overlapped GET 
                var helper = amountOfData * i;
                modifier.Skip = int.Parse(helper.ToString());
                dataIteration.Add((int)modifier.Skip);
                watch.Restart();
                await _instance.GetAsync(modifier);
                watch.Stop();
                cacheSize.AfterQueryExecution = _databaseService.GetCacheItemsCount();

                overlappedCache.Add(watch.ElapsedMilliseconds / 1000.0);
                cacheSizes.Add(cacheSize);
                overlapping.Add(dataIteration);
            }

            var result = new Scenario5Results()
            {
                originalCache = originalCache,
                overlappedCache = overlappedCache,
                skipped = overlapping,
                CacheSizeComparison = cacheSizes
            };

            return result;
        }
    }
}