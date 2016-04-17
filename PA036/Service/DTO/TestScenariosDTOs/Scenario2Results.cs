using System.Collections.Generic;

namespace Service.DTO.TestScenariosDTOs
{
    public class Scenario2Results : ITestResult
    {
        public List<double> CachedQueriesTimes { get; set; }
        public List<double> NotCachedQueriesTimes { get; set; }
        public List<double> XAxis { get; set; }

        public List<CacheSizeComparison> CacheSizeComparison { get; set; }
    }


    public class CacheSizeComparison
    {
        public int BeforeQueryExecution { get; set; }

        public int AfterQueryExecution { get; set; }

        public int NoOfObjectsReturnedInQuery { get; set; }
    }
}