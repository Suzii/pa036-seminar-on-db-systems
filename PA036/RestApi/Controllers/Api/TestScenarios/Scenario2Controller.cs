using System.Threading.Tasks;
using System.Web.Http;
using Service.DTO.TestResultDTOs;
using Service.TestScenarios;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario2Controller : ApiController
    {
        private readonly ITestScenarioService _instance;
        
        public Scenario2Controller()
        {
            _instance = new Scenario2Service();
        }

        public async Task<ITestResult> Get()
        {
            return await _instance.ExecuteTest();
        }
    }
}
