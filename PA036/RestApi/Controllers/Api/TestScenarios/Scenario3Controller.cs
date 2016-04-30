using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;
using Service.DTO.TestScenariosConfigs;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario3Controller : ApiController
    {
        private ITestScenarioService _instance;
        private ITestScenarioService _instance2;
        private ITestScenarioService _instance3;

        public async Task<ITestResult> Get(bool useCloudDatabase = false)
        {
            var config = new Scenario1Config()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance = new Scenario3ServiceUpdate(config);
            return await _instance.ExecuteTest();
        }

        public async Task<ITestResult> Delete(bool useCloudDatabase = false)
        {
            var config = new Scenario1Config()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance2 = new Scenario3ServiceDelete(config);
            return await _instance2.ExecuteTest();
        }

        public async Task<ITestResult> Post(bool useCloudDatabase = false)
        {
            var config = new Scenario1Config()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance3 = new Scenario3ServicePost(config);
            return await _instance2.ExecuteTest();
        }
    }
}
