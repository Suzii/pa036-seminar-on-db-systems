using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Extensions;

namespace Service.TestScenarios
{
    public class Scenario5AdjustedService : ITestScenarioService
    {
        private readonly int _numberOfExecutions;

        private readonly Scenario5Service _service;

        /// <summary>
        /// Executes TestScenario multiple times (depending on configuration), 
        /// computes average values and returns them
        /// </summary>
        /// <param name="config">Test scenario configuration</param>
        public Scenario5AdjustedService(AdjustedScenarioConfig config)
        {
            _numberOfExecutions = config.NumberOfExecutions;
            _service = new Scenario5Service(config);
        }

        public async Task<ITestResult> ExecuteTest()
        {
            var adjustedResults = new Scenario5Results();
            var simpleResults = await ScenarioMultipleExecution();


            adjustedResults.CacheSizeComparison = simpleResults.First().CacheSizeComparison;

            adjustedResults.Skipped = simpleResults.First().Skipped;
            
            adjustedResults.OriginalCache = simpleResults.Select(result => result.OriginalCache).ZippedAverage().ToList();

            adjustedResults.OverlappedCache = simpleResults.Select(result => result.OverlappedCache).ZippedAverage().ToList();

            return adjustedResults;
        }

        private async Task<List<Scenario5Results>> ScenarioMultipleExecution()
        {
            var simpleResults = new List<Scenario5Results>();

            for (var i = 0; i < _numberOfExecutions; i++)
            {
                var simpleResult = (Scenario5Results) await _service.ExecuteTest();

                simpleResults.Add(simpleResult);
            }

            return simpleResults;
        }
    }
}