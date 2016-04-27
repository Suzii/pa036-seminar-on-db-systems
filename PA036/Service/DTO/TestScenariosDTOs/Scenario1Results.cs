using System.Collections.Generic;

namespace Service.DTO.TestScenariosDTOs
{
    public class Scenario1Results : ITestResult
    {
        public List<double> CachedQueriesTimes { get; set; }
        public List<double> XAxis { get; set; }

        public List<CacheSizeComparison> CacheSizeComparison { get; set; }
    }
}