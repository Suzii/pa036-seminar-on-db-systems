using System;
using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario5AdjustedController : ApiController
    {
        public async Task<ITestResult> Get(int numberOfExecutions, bool useCloudDatabase = false)
        {
            var config = new AdjustedScenarioConfig()
            {
                UseRemoteDb = useCloudDatabase,
                NumberOfExecutions = numberOfExecutions
            };

            var instance = new Scenario5AdjustedService(config);
            return await instance.ExecuteTest();
        }
    }
}
