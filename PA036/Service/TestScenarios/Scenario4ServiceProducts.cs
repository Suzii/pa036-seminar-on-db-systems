using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario4ServiceProducts : Scenario4Service, ITestScenarioService
    {
        public Scenario4ServiceProducts(Scenario1Config config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await testUpdateProductsTable();
        }

        /// <summary> 
        /// Simple Get query on table of products
        /// to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        private async Task<Scenario3Results> testUpdateProductsTable()
        {
            _databaseService.InvalidateCache();

            productFilter.OrderDesc = true;
            productFilter.OrderProperty = "id";

            _databaseService.InvalidateCache();
            var data = (await init_product_cache());
            var dataStore = (await init_store_cache());
            var cached = _databaseService.GetCacheItemsCount();

            data[0].StockCount += 1;
            await _instanceProducts.UpdateAsync(data[0]);
            var countInCache = _databaseService.GetCacheItemsCount();

            data[0].StockCount -= 1;
            await _instanceProducts.UpdateAsync(data[0]);
            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }
    }
}