using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario1Controller : ApiController
    {
        public async Task<ITestResult> Get(bool useCloudDatabase = false)
        {
            var config = new ScenarioConfig()
            {
                UseRemoteDb = useCloudDatabase,
            };

            var instance = new Scenario1Service(config);
            return await instance.ExecuteTest();
        }
    }
}
