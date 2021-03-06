﻿using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Filters;
using Shared.Settings;
using Shared.Enums;
using Service.DTO;
using System.Linq;

namespace Service.TestScenarios
{
    public class Scenario7Service : ITestScenarioService
    {
        private readonly IStoreService _storeInstanceWithContextAzure;
        private readonly IStoreService _storeInstanceWithContextLocal;
        private readonly IDatabaseService _databaseService;

        public Scenario7Service()
        {
            _storeInstanceWithContextAzure = new StoreService(new DbSettings { AppContext = AppContexts.Azure });
            _storeInstanceWithContextLocal = new StoreService(new DbSettings { AppContext = AppContexts.LocalNamedAsAzure });
            _databaseService = new DatabaseService();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return  await TestAzureVersusLocalWithSameName();
        }

        /// <summary> 
        /// Test of behavior when two databases are named equally
        /// Simple GET requests from cache on two diffent object
        /// each from another database
        /// </summary>
        /// <returns>Returned objects from both databases</returns>
        public async Task<Scenario7Results> TestAzureVersusLocalWithSameName()
        {
            var storeFilter = new StoreFilter { NameFilter = "Amazing test" };
            var storeResultCleanUp1 = (await _storeInstanceWithContextAzure.GetAsync(storeFilter));
            _databaseService.InvalidateCache();
            var storeResultCleanUp2 = (await _storeInstanceWithContextLocal.GetAsync(storeFilter));

            foreach (var item in storeResultCleanUp1)
                await _storeInstanceWithContextAzure.DeleteAsync(item.Id);

            _databaseService.InvalidateCache();
            foreach (var item in storeResultCleanUp2)
                await _storeInstanceWithContextLocal.DeleteAsync(item.Id);

            var store1 = new StoreDTO() { Name = "Amazing test", City = "Brno", State = "Czech Republic" };
            var store2 = new StoreDTO() { Name = "Amazing test", City = "Zilina", State = "Slovakia" };

            await _storeInstanceWithContextAzure.CreateAsync(store1);
            await _storeInstanceWithContextLocal.CreateAsync(store2);

            var storeResult1 = (await _storeInstanceWithContextAzure.GetAsync(storeFilter)).Single();
            var storeResult2 = (await _storeInstanceWithContextLocal.GetAsync(storeFilter)).Single();

            var result = new Scenario7Results() {
                ObjectFromAzure = storeResult1,
                ObjectFromLocal = storeResult2
            };

            // cleanup
            await _storeInstanceWithContextAzure.DeleteAsync(storeResult1.Id);
            _databaseService.InvalidateCache();
            storeResult2 = (await _storeInstanceWithContextLocal.GetAsync(storeFilter)).Single();
            await _storeInstanceWithContextLocal.DeleteAsync(storeResult2.Id);
            return result;
        }
    }
}