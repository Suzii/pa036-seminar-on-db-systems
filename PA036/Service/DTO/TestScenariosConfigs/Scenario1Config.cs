using Service.DTO.TestScenariosDTOs;

namespace Service.DTO.TestScenariosConfigs
{
    public class Scenario1Config: ITestScenarioConfig
    {
        public bool UseRemoteDb { get; set; }
    }
}