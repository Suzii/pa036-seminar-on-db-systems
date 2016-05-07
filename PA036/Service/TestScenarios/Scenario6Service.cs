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
    public class Scenario6Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private readonly Scenario1Config _config;

        public Scenario6Service(Scenario1Config config)
        {
            _config = config;
            var dbSettings = new DbSettings() { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
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
            return  await getMaxCacheSize();
        }

        /// <summary> 
        /// Maximazing number of elements in cache
        /// </summary>
        /// <returns>Number of elements in cache</returns>
        private async Task<Scenario6Results> getMaxCacheSize()
        {
            var modifier = new ProductFilter();
            modifier.OrderDesc = true;
            modifier.OrderProperty = "id";
            var totalCount = await _instance.TotalCountAsync();
            var overlappedCache = new List<double>();

            var cacheSize = 0;

            _databaseService.InvalidateCache();

            for (var i = 1; i < totalCount; i++)
            {
                modifier.Take = i;

                for (var j = 0; j < totalCount - i; j++)
                {
                    modifier.Skip = j;
                    await _instance.GetAsync(modifier);
                    cacheSize = _databaseService.GetCacheItemsCount();

                }
            }

            var result = new Scenario6Results()
            {
                itemsInCache = cacheSize
            };

            return result;
        }
    }
}