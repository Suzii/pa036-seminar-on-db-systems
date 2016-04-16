using System.Threading.Tasks;
using Service.DTO.TestResultDTOs;

namespace Service.TestScenarios
{
    public interface ITestScenarioService
    {
        Task<ITestResult> ExecuteTest();
    }
}
