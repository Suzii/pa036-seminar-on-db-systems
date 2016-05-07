using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario3ServiceDelete : Scenario3Service, ITestScenarioService 
    {
        public Scenario3ServiceDelete(Scenario1Config config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await testDeleteData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then delete some of them
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        private async Task<Scenario3Results> testDeleteData()
        {
            _databaseService.InvalidateCache();
            var data = await init_cache();
            var cached = _databaseService.GetCacheItemsCount();

            await _instance.DeleteAsync(data[0].Id);
            var countInCache = _databaseService.GetCacheItemsCount();

            await _instance.CreateAsync(data[0]);

            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }


    }
}