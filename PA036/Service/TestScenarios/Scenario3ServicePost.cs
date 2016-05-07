using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;
using DataAccess.Model;
using Service.DTO;

namespace Service.TestScenarios
{
    public class Scenario3ServicePost : Scenario3Service, ITestScenarioService
    {

        public Scenario3ServicePost(ScenarioConfig config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await TestPostData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        /// </summary>
        private async Task<Scenario3Results> TestPostData()
        {
            DatabaseService.InvalidateCache();
            ProductFilter.OrderDesc = true;
            ProductFilter.OrderProperty = "id";
            var data = (await InitCache());
            var cached = DatabaseService.GetCacheItemsCount();
            ProductDTO product = new ProductDTO() { Name = "test", StockCount = 666, UnitCost = 666 };


            await Instance.CreateAsync(product);
            var countInCache = DatabaseService.GetCacheItemsCount();
            ProductFilter.Take = 1;
            ProductFilter.OrderDesc = true;
            ProductFilter.OrderProperty = "id";
            ProductFilter.Skip = 0;
            ProductFilter.UnitCostFilter = 666;
            ProductFilter.NameFilter = "test";
            ProductFilter.UnitCostFilter = 666;
            var last = await Instance.GetAsync(ProductFilter);
            if (last.Count != 0) { 
                await Instance.DeleteAsync(last[0].Id);
            }
            return new Scenario3Results()
            {
                BeforeAction = cached,
                AfterAction = countInCache
            };
        }

    }
}