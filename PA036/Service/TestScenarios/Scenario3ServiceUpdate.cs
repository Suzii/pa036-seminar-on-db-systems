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
    public class Scenario3ServiceUpdate : Scenario3Service, ITestScenarioService
    {

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
            var data = (await init_cache());
            var cached = _databaseService.GetCacheItemsCount();
            for (var i = 0; i < data.Count/5; i++)
            {
                data[i].StockCount += 1;
                await _instance.UpdateAsync(data[i]);
            }
            var countInCache = _databaseService.GetCacheItemsCount();

            for (var i = 0; i < data.Count/5; i++)
            {
                data[i].StockCount -= 1;
                await _instance.UpdateAsync(data[i]);
            }
            return new Scenario3Results()
            {
                beforeAction = cached,
                afterAction = countInCache
            };
        }

    }
}