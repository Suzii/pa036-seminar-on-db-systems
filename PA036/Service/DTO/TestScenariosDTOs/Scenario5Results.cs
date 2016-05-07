using System.Collections.Generic;

namespace Service.DTO.TestScenariosDTOs
{
    public class Scenario5Results : ITestResult
    {
        public List<double> OriginalCache  { get; set; }

        public List<double> OverlappedCache { get; set; }

        public List<List<int>> Skipped { get; set; }

        public List<CacheSizeComparison> CacheSizeComparison { get; set; }
    }
}