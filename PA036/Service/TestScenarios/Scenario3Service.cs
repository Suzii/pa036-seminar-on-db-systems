using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;

namespace Service.TestScenarios
{
    public class Scenario3Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;

        public Scenario3Service()
        {
            var dbSettings = new DbSettings() { UseSecondAppContext = true };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return await testUpdateData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        private async Task<Scenario3Results> testUpdateData()
        {
            _databaseService.InvalidateCache();
            var totalCount = await _instance.TotalCountAsync();
            var modifier = new ProductFilter();
            var output = new Dictionary<string, IList<double>>();
            var withoutCache = new List<double>();
            var withCache = new List<double>();

            modifier.Take = 1000;
            var data = await _instance.GetAsync(modifier);
            await _instance.GetAsync(modifier);
            var migrationsInCache = 0;
            var cached = _databaseService.GetCacheItemsCount();
            if (cached == 4)
            {
                migrationsInCache = 2;
                cached -= migrationsInCache;
            }

            for (var i = 0; i < 30; i++)
            {
                data[i].StockCount += 1;
                await _instance.UpdateAsync(data[i]);
            }

            var countInCache = _databaseService.GetCacheItemsCount();
            if (migrationsInCache == 2)
            {
                countInCache -= migrationsInCache;
            }
            var result = new Scenario3Results()
            {
                beforeUpdate = cached,
                afterUpdate = countInCache
            };
            return result;
        }

    }
}