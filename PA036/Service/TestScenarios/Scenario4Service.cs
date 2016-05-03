using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;
using Service.DTO.TestScenariosConfigs;
using DataAccess.Model;
using Service.DTO;
using Service.Data;
using Service.Config;
using Shared.Filters;
using Shared.Settings;
using System.Collections.Generic;

namespace Service.TestScenarios
{
    public class Scenario4Service
    {
        protected readonly IProductService _instanceProducts;
        protected readonly IStoreService _instanceStores;
        protected readonly IDatabaseService _databaseService;
        protected ProductFilter productFilter;
        protected StoreFilter storeFilter;
        protected readonly Scenario1Config _config;
        public Scenario4Service(Scenario1Config config)
        {
            _config = config;
            var dbSettings = new DbSettings() { UseSecondAppContext = config.UseRemoteDb };
            _instanceProducts = new ProductService(dbSettings);
            _instanceStores = new StoreService(dbSettings);
            _databaseService = new DatabaseService();
            productFilter = new ProductFilter();
            storeFilter = new StoreFilter();
            productFilter.Take = 1;
            storeFilter.Take = 1;
            _instanceProducts.Get(productFilter);
            _instanceProducts.Get(productFilter);
            _instanceStores.Get(storeFilter);
            _instanceStores.Get(storeFilter);
            _databaseService.InvalidateCache();
            productFilter.Take = 0;
            storeFilter.Take = 0;
        }

        protected async Task<IList<ProductDTO>> init_product_cache()
        {
            productFilter.Take = 100;

            var data = (await _instanceProducts.GetAsync(productFilter));
            await _instanceProducts.GetAsync(productFilter);
            productFilter.Take = 50;

            data = (await _instanceProducts.GetAsync(productFilter));
            await _instanceProducts.GetAsync(productFilter);
            return data;
        }

        protected async Task<IList<StoreDTO>> init_store_cache()
        {
            storeFilter.Take = 100;

            var data = (await _instanceStores.GetAsync(storeFilter));
            await _instanceStores.GetAsync(storeFilter);
            return data;
        }
    }
}