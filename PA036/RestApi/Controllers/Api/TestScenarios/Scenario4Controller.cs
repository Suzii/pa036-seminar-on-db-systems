using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;
using Service.DTO.TestScenariosConfigs;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario4Controller : ApiController
    {
        private ITestScenarioService _instance;
        private ITestScenarioService _instance2;

        //store
        public async Task<ITestResult> Get(bool useCloudDatabase = false)
        {
            var config = new ScenarioConfig()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance = new Scenario4ServiceStores(config);
            return await _instance.ExecuteTest();
        }

        //products
        public async Task<ITestResult> Post(bool useCloudDatabase = false)
        {
            var config = new ScenarioConfig()
            {
                UseRemoteDb = useCloudDatabase,
            };
            _instance2 = new Scenario4ServiceProducts(config);
            return await _instance2.ExecuteTest();
        }
    }
}
