using Service.DTO.TestScenariosDTOs;

namespace Service.DTO.TestScenariosConfigs
{
    public class Scenario2Config: ScenarioConfig
    {
        public bool InvalidateCacheAfterIteration { get; set; }

        public bool DoNotCacheItems { get; set; }

        public bool IncreasingSizeOfRequest { get; set; }
    }
}