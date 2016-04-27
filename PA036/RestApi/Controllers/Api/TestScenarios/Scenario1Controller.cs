using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario1Controller : ApiController
    {
        public async Task<ITestResult> Get(bool useCloudDatabase = false, bool invalidateCache = false)
        {
            var config = new Scenario2Config()
            {
                UseRemoteDb = useCloudDatabase,
                InvalidateCacheAfterIteration = invalidateCache
            };

            var instance = new Scenario1Service(config);
            return await instance.ExecuteTest();
        }
    }
}
