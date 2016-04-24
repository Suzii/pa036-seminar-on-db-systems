using Service.DTO.TestScenariosDTOs;

namespace Service.DTO.TestScenariosConfigs
{
    public class Scenario2Config: ITestScenarioConfig
    {
        public bool UseRemoteDb { get; set; }

        public bool InvalidateCacheAfterIteration { get; set; }

        public bool DoNotCacheItems { get; set; }
    }
}