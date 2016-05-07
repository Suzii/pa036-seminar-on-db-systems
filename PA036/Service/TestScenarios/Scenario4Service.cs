using System.Threading.Tasks;
using Service.DTO.TestScenariosConfigs;
using Service.DTO;
using Service.Data;
using Service.Config;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;
using Shared.Enums;

namespace Service.TestScenarios
{
    public class Scenario4Service
    {
        protected readonly IProductService InstanceProducts;
        protected readonly IStoreService InstanceStores;
        protected readonly IDatabaseService DatabaseService;
        protected ProductFilter ProductFilter;
        protected StoreFilter StoreFilter;
        protected readonly ScenarioConfig Config;

        public Scenario4Service(ScenarioConfig config)
        {
            Config = config;
            var dbSettings = new DbSettings() { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
            InstanceProducts = new ProductService(dbSettings);
            InstanceStores = new StoreService(dbSettings);
            DatabaseService = new DatabaseService();
            ProductFilter = new ProductFilter();
            StoreFilter = new StoreFilter();

            ProductFilter.Take = 1;
            StoreFilter.Take = 1;

            InstanceProducts.Get(ProductFilter);
            InstanceProducts.Get(ProductFilter);
            InstanceStores.Get(StoreFilter);
            InstanceStores.Get(StoreFilter);
            DatabaseService.InvalidateCache();

            ProductFilter.Take = 0;
            StoreFilter.Take = 0;
        }

        /// <summary>
        /// Initialization of cache for Product table
        /// </summary>
        /// <returns>cached data</returns>
        protected async Task<IList<ProductDTO>> InitProductCache()
        {
            ProductFilter.Take = 100;

            var data = (await InstanceProducts.GetAsync(ProductFilter));
            await InstanceProducts.GetAsync(ProductFilter);
            ProductFilter.Take = 50;

            data = (await InstanceProducts.GetAsync(ProductFilter));
            await InstanceProducts.GetAsync(ProductFilter);
            return data;
        }

        /// <summary>
        /// Initialization of cache for Store table
        /// </summary>
        /// <returns>cached data</returns>
        protected async Task<IList<StoreDTO>> InitStoreCache()
        {
            StoreFilter.Take = 100;

            var data = (await InstanceStores.GetAsync(StoreFilter));
            await InstanceStores.GetAsync(StoreFilter);
            return data;
        }
    }
}