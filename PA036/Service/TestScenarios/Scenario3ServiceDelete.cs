using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;
using Service.DTO;

namespace Service.TestScenarios
{
    public class Scenario3ServiceDelete : Scenario3Service, ITestScenarioService 
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;
        private ProductFilter productFilter;

        public Scenario3ServiceDelete()
        {
            var dbSettings = new DbSettings() { UseSecondAppContext = true };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
            productFilter = new ProductFilter();
            productFilter.Take = 1;
            _instance.Get(productFilter);
            _instance.Get(productFilter);
            _databaseService.InvalidateCache();
            productFilter.Take = 0;
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return await testDeleteData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        private async Task<Scenario3Results> testDeleteData()
        {
            _databaseService.InvalidateCache();
            var data = await init_cache();
            var cached = _databaseService.GetCacheItemsCount();

            for (var i = 0; i < data.Count/20; i++)
            {
                await _instance.DeleteAsync(data[i].Id);
            }
            var countInCache = _databaseService.GetCacheItemsCount();

            for  (var i = 0; i < data.Count/20; i++)
            {
                await _instance.CreateAsync(data[i]);
            }

            return new Scenario3Results()
            {
                beforeAction = cached,
                afterAction = countInCache
            };
        }


    }
}