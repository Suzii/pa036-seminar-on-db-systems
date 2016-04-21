using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario3Controller : ApiController
    {
        private readonly ITestScenarioService _instance;
        private readonly ITestScenarioService _instance2;

        public Scenario3Controller()
        {
            _instance = new Scenario3ServiceUpdate();
            _instance2 = new Scenario3ServiceDelete();
        }

        public async Task<ITestResult> Get()
        {
            return await _instance.ExecuteTest();
        }
        public async Task<ITestResult> Delete()
        {
            return await _instance2.ExecuteTest();
        }
    }
}
