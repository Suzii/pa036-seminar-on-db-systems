using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario4ServiceStores : Scenario4Service, ITestScenarioService
    {
        public Scenario4ServiceStores(Scenario1Config config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await testUpdateStoreTable();
        }

        /// <summary> 
        /// Simple Get query on table of stores
        /// to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        private async Task<Scenario3Results> testUpdateStoreTable()
        {
            _databaseService.InvalidateCache();

            productFilter.OrderDesc = true;
            productFilter.OrderProperty = "id";

            _databaseService.InvalidateCache();
            var data = (await init_product_cache());
            var dataStore = (await init_store_cache());
            var cached = _databaseService.GetCacheItemsCount();
            var backup = dataStore[0].City;
            dataStore[0].City = "Valhalla";
            await _instanceStores.UpdateAsync(dataStore[0]);
            var countInCache = _databaseService.GetCacheItemsCount();

            dataStore[0].City = backup;
            await _instanceStores.UpdateAsync(dataStore[0]);
            return new Scenario3Results()
            {
                beforeAction = cached,
                afterAction = countInCache
            };
        }
    }
}