using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Service.Config;
using Service.Data;
using Service.DTO.TestResultDTOs;
using Shared.Filters;
using Shared.Settings;

namespace Service.TestScenarios
{
    public class Scenario2Service : ITestScenarioService
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;

        public Scenario2Service()
        {
            var dbSettings = new DbSettings() { UseSecondAppContext = true };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
        }

        public async Task<ITestResult> ExecuteTest()
        {
            return await GetsIncreasingCount();
        }

        /// <summary> 
        /// Simple GET query for k products executed by two users.
        /// 
        /// Number k is increased in every iteration
        /// Cache is invalidated before every get request of user A. 
        /// </summary>
        /// <returns>Measured data of both users</returns>
        private async Task<Scenario2Results> GetsIncreasingCount()
        {
            var totalCount = await _instance.TotalCountAsync();
            var modifier = new ProductFilter();
            var withoutCache = new List<double>();
            var withCache = new List<double>();
            const int step = 1000;

            for (var i = step; i <= totalCount; i += step)
            {
                _databaseService.InvalidateCache();

                modifier.Take = i;
                var watch = Stopwatch.StartNew();
                await _instance.GetAsync(modifier);
                watch.Stop();
                withoutCache.Add(watch.ElapsedMilliseconds / 1000.0);

                watch.Restart();
                await _instance.GetAsync(modifier);
                watch.Stop();
                withCache.Add(watch.ElapsedMilliseconds / 1000.0);
            }

            var result = new Scenario2Results()
            {
                cached = withCache,
                notCached = withoutCache,
                xAxis = new List<double>() { 0, totalCount + step, step }
            };

            return result;
        }

    }
}