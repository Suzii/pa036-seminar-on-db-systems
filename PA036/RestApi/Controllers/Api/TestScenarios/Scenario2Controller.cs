using System;
using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario2Controller : ApiController
    {
        public async Task<ITestResult> Get(bool useCloudDatabase = false, bool invalidateCache = false, bool doNotCacheItems = false, bool getsSizeIncreasing = false)
        {
            var config = new Scenario2Config
            {
                UseRemoteDb = useCloudDatabase,
                InvalidateCacheAfterIteration = invalidateCache,
                DoNotCacheItems = doNotCacheItems,
                IncreasingSizeOfRequest = getsSizeIncreasing
            };

            var instance = new Scenario2Service(config);
            return await instance.ExecuteTest();
        }
    }
}
