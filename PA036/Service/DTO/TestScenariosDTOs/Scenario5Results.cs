using System;
using System.Collections.Generic;

namespace Service.DTO.TestScenariosDTOs
{
    public class Scenario5Results : ITestResult
    {
        public List<double> originalCache  { get; set; }
        public List<double> overlappedCache { get; set; }
        public List<List<int>> skipped { get; set; }

        public List<CacheSizeComparison> CacheSizeComparison { get; set; }
    }
}