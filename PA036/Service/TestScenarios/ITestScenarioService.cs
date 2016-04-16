using System.Threading.Tasks;
using Service.DTO.TestScenariosDTOs;

namespace Service.TestScenarios
{
    public interface ITestScenarioService
    {
        Task<ITestResult> ExecuteTest();
    }
}
