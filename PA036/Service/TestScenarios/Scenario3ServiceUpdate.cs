using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario3ServiceUpdate : Scenario3Service, ITestScenarioService
    {

        public Scenario3ServiceUpdate(Scenario1Config config) : base(config) { }

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
            data[0].StockCount += 1;
            await _instance.UpdateAsync(data[0]);
            var countInCache = _databaseService.GetCacheItemsCount();

            data[0].StockCount -= 1;
            await _instance.UpdateAsync(data[0]);
            return new Scenario3Results()
            {
                beforeAction = cached,
                afterAction = countInCache
            };
        }

    }
}