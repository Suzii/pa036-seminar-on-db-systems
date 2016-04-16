using System.Collections.Generic;

namespace Service.DTO.TestScenariosDTOs
{
    public class Scenario2Results : ITestResult
    {
        public List<double> cached { get; set; }
        public List<double> notCached { get; set; }
        public List<double> xAxis { get; set; }
    }
}