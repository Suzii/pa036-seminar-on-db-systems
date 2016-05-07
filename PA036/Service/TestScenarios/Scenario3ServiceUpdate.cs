using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario3ServiceUpdate : Scenario3Service, ITestScenarioService
    {

        public Scenario3ServiceUpdate(ScenarioConfig config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await TestUpdateData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        /// </summary>
        private async Task<Scenario3Results> TestUpdateData()
        {
            DatabaseService.InvalidateCache();
            var data = (await InitCache());
            var cached = DatabaseService.GetCacheItemsCount();
            data[0].StockCount += 1;
            await Instance.UpdateAsync(data[0]);
            var countInCache = DatabaseService.GetCacheItemsCount();

            data[0].StockCount -= 1;
            await Instance.UpdateAsync(data[0]);
            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }

    }
}