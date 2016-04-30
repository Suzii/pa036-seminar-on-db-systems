﻿using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;
using DataAccess.Model;
using Service.DTO;

namespace Service.TestScenarios
{
    public class Scenario3ServicePost : Scenario3Service, ITestScenarioService
    {

        public Scenario3ServicePost(Scenario1Config config) : base(config) { }

        public async Task<ITestResult> ExecuteTest()
        {
            return await testPostData();
        }

        /// <summary> 
        /// Simple Get query to cache results and then updating 
        /// some of them.
        /// Testing if change of data in database will invalidate cache
        private async Task<Scenario3Results> testPostData()
        {
            _databaseService.InvalidateCache();
            productFilter.OrderDesc = true;
            productFilter.OrderProperty = "id";
            var data = (await init_cache());
            var cached = _databaseService.GetCacheItemsCount();
            ProductDTO product = new ProductDTO() {Name="test", StockCount=666, UnitCost=666};


            await _instance.CreateAsync( product );
            var countInCache = _databaseService.GetCacheItemsCount();

            await _instance.DeleteAsync(product.Id);
            return new Scenario3Results()
            {
                beforeAction = cached,
                afterAction = countInCache
            };
        }

    }
}