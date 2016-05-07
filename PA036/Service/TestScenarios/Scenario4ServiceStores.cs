using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario4ServiceStores : Scenario4Service, ITestScenarioService
    {
        public Scenario4ServiceStores(ScenarioConfig config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await TestUpdateStoreTable();
        }

        /// <summary> 
        /// Simple Get query on table of stores
        /// to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        /// </summary>
        private async Task<Scenario3Results> TestUpdateStoreTable()
        {
            DatabaseService.InvalidateCache();

            ProductFilter.OrderDesc = true;
            ProductFilter.OrderProperty = "id";

            DatabaseService.InvalidateCache();
            var data = (await InitProductCache());
            var dataStore = (await InitStoreCache());
            var cached = DatabaseService.GetCacheItemsCount();
            var backup = dataStore[0].City;
            dataStore[0].City = "Valhalla";
            await InstanceStores.UpdateAsync(dataStore[0]);
            var countInCache = DatabaseService.GetCacheItemsCount();

            dataStore[0].City = backup;
            await InstanceStores.UpdateAsync(dataStore[0]);

            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }
    }
}