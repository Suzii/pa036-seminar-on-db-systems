using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;

namespace Service.TestScenarios
{
    public class Scenario4ServiceProducts : Scenario4Service, ITestScenarioService
    {
        public Scenario4ServiceProducts(ScenarioConfig config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await TestUpdateProductsTable();
        }

        /// <summary> 
        /// Simple Get query on table of products
        /// to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        /// </summary>
        private async Task<Scenario3Results> TestUpdateProductsTable()
        {
            DatabaseService.InvalidateCache();

            ProductFilter.OrderDesc = true;
            ProductFilter.OrderProperty = "id";

            DatabaseService.InvalidateCache();
            var data = (await InitProductCache());
            var dataStore = (await InitStoreCache());
            var cached = DatabaseService.GetCacheItemsCount();

            data[0].StockCount += 1;
            await InstanceProducts.UpdateAsync(data[0]);
            var countInCache = DatabaseService.GetCacheItemsCount();

            data[0].StockCount -= 1;
            await InstanceProducts.UpdateAsync(data[0]);

            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }
    }
}