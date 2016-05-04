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
        protected readonly IProductService _instance;
        protected readonly IDatabaseService _databaseService;
        protected ProductFilter productFilter;
        protected readonly Scenario1Config _config;

        public Scenario3Service(Scenario1Config config)
        {
            _config = config;
            var dbSettings = new DbSettings() { AppContext = config.UseRemoteDb ? AppContexts.Azure : AppContexts.Local };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
            productFilter = new ProductFilter();
            productFilter.Take = 1;
            _instance.Get(productFilter);
            _instance.Get(productFilter);
            _databaseService.InvalidateCache();
            productFilter.Take = 0;
        }

        protected async Task<IList<ProductDTO>> init_cache()
        {
            _databaseService.InvalidateCache();

            productFilter.Take = 100;
            
            var data = (await _instance.GetAsync(productFilter));
            await _instance.GetAsync(productFilter);
            return data;
        }
    }
}