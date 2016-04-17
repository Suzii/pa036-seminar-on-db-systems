using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestScenariosDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario3Controller : ApiController
    {
        private readonly ITestScenarioService _instance;

        public Scenario3Controller()
        {
            _instance = new Scenario3Service();
        }

        public async Task<ITestResult> Get()
        {
            return await _instance.ExecuteTest();
        }
    }
}
