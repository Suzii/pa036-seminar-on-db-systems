namespace Service.DTO.TestScenariosDTOs
{
    public class Scenario7Results : ITestResult
    {
        public StoreDTO ObjectFromAzure { get; set; }

        public StoreDTO ObjectFromLocal { get; set; }
    }
}