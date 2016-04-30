using System;
using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario2AdjustedController : ApiController
    {
        public async Task<ITestResult> Get(bool useCloudDatabase = false, bool invalidateCache = false, bool doNotCacheItems = false, bool getsSizeIncreasing = false)
        {
            var config = new Scenario2Config()
            {
                UseRemoteDb = useCloudDatabase,
                InvalidateCacheAfterIteration = invalidateCache,
                DoNotCacheItems = doNotCacheItems,
                IncreasingSizeOfRequest = getsSizeIncreasing
            };

            var instance = new Scenario2AdjustedService(config);
            return await instance.ExecuteTest();
        }
    }
}
