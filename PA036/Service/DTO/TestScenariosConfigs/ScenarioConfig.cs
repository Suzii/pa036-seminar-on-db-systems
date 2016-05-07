using Service.DTO.TestScenariosDTOs;

namespace Service.DTO.TestScenariosConfigs
{
    public class ScenarioConfig: ITestScenarioConfig
    {
        public bool UseRemoteDb { get; set; }
    }
}