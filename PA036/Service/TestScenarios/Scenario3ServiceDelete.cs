using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario3ServiceDelete : Scenario3Service, ITestScenarioService 
    {
        public Scenario3ServiceDelete(ScenarioConfig config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await TestDeleteData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then delete some of them
        /// Testing if change of data in database will invalidate cache
        /// </summary>
        private async Task<Scenario3Results> TestDeleteData()
        {
            DatabaseService.InvalidateCache();
            var data = await InitCache();
            var cached = DatabaseService.GetCacheItemsCount();

            await Instance.DeleteAsync(data[0].Id);
            var countInCache = DatabaseService.GetCacheItemsCount();

            var deleted = data[0];
            deleted.Id = 0;
            await Instance.CreateAsync(deleted);

            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }


    }
}