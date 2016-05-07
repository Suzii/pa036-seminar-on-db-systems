using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Shared.Filters;
using Shared.Settings;
using Service.DTO;
using Service.DTO.TestScenariosConfigs;
using Shared.Enums;

namespace Service.TestScenarios
{
    public class Scenario3Service
    {
        protected readonly IProductService Instance;
        protected readonly IDatabaseService DatabaseService;
        protected ProductFilter ProductFilter;
        protected readonly ScenarioConfig Config;

        public Scenario3Service(ScenarioConfig config)
        {
            Config = config;
            var dbSettings = new DbSettings() { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
            Instance = new ProductService(dbSettings);
            DatabaseService = new DatabaseService();
            ProductFilter = new ProductFilter {Take = 1};

            Instance.Get(ProductFilter);
            Instance.Get(ProductFilter);
            DatabaseService.InvalidateCache();
            ProductFilter.Take = 0;
        }

        /// <summary>
        /// Initialization of cache
        /// </summary>
        /// <returns>cached data</returns>
        protected async Task<IList<ProductDTO>> InitCache()
        {
            DatabaseService.InvalidateCache();

            ProductFilter.Take = 100;
            
            var data = (await Instance.GetAsync(ProductFilter));
            await Instance.GetAsync(ProductFilter);
            return data;
        }
    }
}