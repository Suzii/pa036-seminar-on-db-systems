using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.DTO.TestScenariosConfigs;
using Service.DTO.TestScenariosDTOs;
using Shared.Extensions;

namespace Service.TestScenarios
{
    public class Scenario2AdjustedService : ITestScenarioService
    {
        private const int NumberOfExecutions = 5;

        private readonly Scenario2Service _service;

        /// <summary>
        /// Executes TestScenario2 5 times, computes average values and returns them
        /// </summary>
        /// <param name="config"></param>
        public Scenario2AdjustedService(AdjustedScenario2Config config)
        {
            _service = new Scenario2Service(config);
        }

        public async Task<ITestResult> ExecuteTest()
        {
            var adjustedResults = new Scenario2Results();
            var simpleResults = await ScenarioMultipleExecution();

            adjustedResults.XAxis = simpleResults.First().XAxis;

            adjustedResults.CacheSizeComparison = simpleResults.First().CacheSizeComparison;
            
            adjustedResults.CachedQueriesTimes = simpleResults.Select(result => result.CachedQueriesTimes).ZippedAverage().ToList();

            adjustedResults.NotCachedQueriesTimes = simpleResults.Select(result => result.NotCachedQueriesTimes).ZippedAverage().ToList();

            return adjustedResults;
        }

        private async Task<List<Scenario2Results>> ScenarioMultipleExecution()
        {
            var simpleResults = new List<Scenario2Results>();

            for (var i = 0; i < NumberOfExecutions; i++)
            {
                var simpleResult = (Scenario2Results) await _service.ExecuteTest();

                simpleResults.Add(simpleResult);
            }

            return simpleResults;
        }
    }
}