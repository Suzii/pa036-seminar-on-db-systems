using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;
using Service.DTO.TestScenariosConfigs;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario6Controller : ApiController
    {
        private ITestScenarioService _instance;

        public async Task<ITestResult> Get(bool useCloudDatabase = false)
        {
            var config = new ScenarioConfig()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance = new Scenario6Service(config);
            return await _instance.ExecuteTest();
        }
    }
}
