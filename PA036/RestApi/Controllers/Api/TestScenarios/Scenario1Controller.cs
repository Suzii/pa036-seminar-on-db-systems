using System.Threading.Tasks;
using Service.Data;
using System;
using System.Diagnostics;
using System.Web.Http;
using System.Collections;
using System.Collections.Generic;
using Shared.Settings;
using Shared.Filters;
using Service.Config;

namespace RestApi.Controllers.Api.TestScenarios
{
    public class Scenario1Controller : ApiController
    {
        private readonly IProductService _instance;
        private readonly IDatabaseService _databaseService;

        public Scenario1Controller()
        {
            var dbSettings = new DbSettings() { UseSecondAppContext = true };
            _instance = new ProductService(dbSettings);
            _databaseService = new DatabaseService();
        }

        public async Task<IDictionary<string, IList<double>>> Get()
        {
            _databaseService.InvalidateCache();
            var totalCount = await _instance.TotalCountAsync();
            var modifier = new ProductFilter();
            var output = new Dictionary<string, IList<double>>();
            var withoutCache = new List<double>();
            var withCache = new List<double>();
            const int step = 1000;

            for (var i = step; i <= totalCount; i += step)
            {
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
            output.Add("notCached", withoutCache);
            output.Add("cached", withCache);
            output.Add("xAxis", new List<double>() { 0, totalCount + step, step });
            return output;
        }

    }
}
