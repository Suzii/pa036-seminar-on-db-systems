using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;
using Service.DTO.TestScenariosConfigs;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario7Controller : ApiController
    {
        private ITestScenarioService _instance;

        public async Task<ITestResult> Get()
        {
            _instance = new Scenario7Service();
            return await _instance.ExecuteTest();
        }
    }
}
