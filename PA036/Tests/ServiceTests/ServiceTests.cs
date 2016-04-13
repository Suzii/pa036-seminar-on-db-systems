using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Data;
using Shared.Filters;
using Shared.Settings;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    [TestClass]
    public class ServiceTests
    {
        private IProductService _instance;

        [TestInitialize]
        public void BeforeMethod()
        {
            _instance = new ProductService();
        }

        [TestMethod]
        public async Task GetData()
        {
            var ticksCount1 = DateTime.Now.Ticks;
            await _instance.GetAllAsync();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            await _instance.GetAllAsync();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            Debug.WriteLine("First execution: {0}", ticksCount1);
            Debug.WriteLine("Second execution: {0}", ticksCount2);
        }

        [TestMethod]
        public async Task TotalCount()
        {
            var count = await _instance.TotalCountAsync();
            Debug.WriteLine("Total count: {0}", count);
        }

        [TestMethod]
        public async Task Context1()
        {
            var settings = new DbSettings() { UseSecondAppContext = false };
            await TestGet(settings);
        }

        [TestMethod]
        public async Task Context2()
        {
            var settings = new DbSettings() { UseSecondAppContext = true };
            await TestGet(settings);
        }

        [TestMethod]
         
        public async Task TestCycleAsync()
        {
            var modifier = new ProductFilter();
            for (var i = 1; i <= 100; i++)
            {
                modifier.Take = i;
                Debug.WriteLine("Async cycle iteration: {0}", i);
                await _instance.GetAsync(modifier);
                Debug.WriteLine("Async cycle done: {0}", i);
            }
        }

        private async Task TestGet(DbSettings settings)
        {
            // Don't run this if database has not enough data
            if (await _instance.TotalCountAsync() < 4)
                return;

            Debug.WriteLine("Use second app context: {0}", settings.UseSecondAppContext);

            var modifier = new ProductFilter();
            modifier.Take = 4;
            var all = await _instance.GetAsync(modifier, settings);

            var ticksCount1 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(0).Id, settings);
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(1).Id, settings);
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            var ticksCount3 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(2).Id, settings);
            ticksCount3 = DateTime.Now.Ticks - ticksCount3;

            var ticksCount4 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(3).Id, settings);
            ticksCount4 = DateTime.Now.Ticks - ticksCount4;

            Debug.WriteLine("First execution: {0}, {1}, {2}, {3}", ticksCount1, ticksCount2, ticksCount3, ticksCount4);

            ticksCount1 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(0).Id, settings);
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            ticksCount2 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(1).Id, settings);
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            ticksCount3 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(2).Id, settings);
            ticksCount3 = DateTime.Now.Ticks - ticksCount3;

            ticksCount4 = DateTime.Now.Ticks;
            await _instance.GetAsync(all.ElementAt(3).Id, settings);
            ticksCount4 = DateTime.Now.Ticks - ticksCount4;

            Debug.WriteLine("Second execution: {0}, {1}, {2}, {3}", ticksCount1, ticksCount2, ticksCount3, ticksCount4);
            Debug.WriteLine("");
        }
    }
}
