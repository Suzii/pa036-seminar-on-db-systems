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

        public Scenario6Service(ScenarioConfig config)
        {
            var dbSettings = new DbSettings { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();

            var productFilter = new ProductFilter {Take = 1};
            _instance.Get(productFilter);
            _instance.Get(productFilter);
            _databaseService.InvalidateCache();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return  await GetMaxCacheSize();
        }

        /// <summary> 
        /// Maximazing number of elements in cache
        /// </summary>
        /// <returns>Number of elements in cache</returns>
        private async Task<Scenario6Results> GetMaxCacheSize()
        {
            var modifier = new ProductFilter
            {
                OrderDesc = true,
                OrderProperty = "id"
            };

            var totalCount = await _instance.TotalCountAsync();

            _databaseService.InvalidateCache();
            var cacheSize = 0;

            for (var i = 1; i < totalCount; i++)
            {
                modifier.Take = i;

                for (var j = 0; j <= totalCount - i; j++)
                {
                    modifier.Skip = j;
                    await _instance.GetAsync(modifier);
                    cacheSize = _databaseService.GetCacheItemsCount();
                }
            }

            var result = new Scenario6Results
            {
                ItemsInCache = cacheSize
            };

            return result;
        }
    }
}