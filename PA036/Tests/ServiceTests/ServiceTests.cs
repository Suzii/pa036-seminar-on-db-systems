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
        private IProductService _instanceWithContext1;
        private IProductService _instanceWithContext2;

        [TestInitialize]
        public void BeforeMethod()
        {
            _instanceWithContext1 = new ProductService(new DbSettings() {UseSecondAppContext = false});
            _instanceWithContext2 = new ProductService(new DbSettings() {UseSecondAppContext = true});
        }

        [TestMethod]
        public async Task GetData()
        {
            var ticksCount1 = DateTime.Now.Ticks;
            await _instanceWithContext1.GetAllAsync();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            await _instanceWithContext1.GetAllAsync();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            Debug.WriteLine("First execution: {0}", ticksCount1);
            Debug.WriteLine("Second execution: {0}", ticksCount2);
        }

        [TestMethod]
        public async Task TotalCount()
        {
            var count = await _instanceWithContext1.TotalCountAsync();
            Debug.WriteLine("Total count: {0}", count);
        }

        [TestMethod]
        public async Task Context1()
        {
            Debug.WriteLine("Use second app context: false");
            await TestGet(_instanceWithContext1);
        }

        [TestMethod]
        public async Task Context2()
        {
            Debug.WriteLine("Use second app context: true");
            await TestGet(_instanceWithContext2);
        }

        [TestMethod]
         
        public async Task TestCycleAsync()
        {
            var modifier = new ProductFilter();
            for (var i = 1; i <= 100; i++)
            {
                modifier.Take = i;
                Debug.WriteLine("Async cycle iteration: {0}", i);
                await _instanceWithContext1.GetAsync(modifier);
                Debug.WriteLine("Async cycle done: {0}", i);
            }
        }

        private async Task TestGet(IProductService instance)
        {
            // Don't run this if database has not enough data
            if (await instance.TotalCountAsync() < 4)
                return;

            var modifier = new ProductFilter {Take = 4};
            var all = await instance.GetAsync(modifier);

            var ticksCount1 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(0).Id);
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(1).Id);
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            var ticksCount3 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(2).Id);
            ticksCount3 = DateTime.Now.Ticks - ticksCount3;

            var ticksCount4 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(3).Id);
            ticksCount4 = DateTime.Now.Ticks - ticksCount4;

            Debug.WriteLine("First execution: {0}, {1}, {2}, {3}", ticksCount1, ticksCount2, ticksCount3, ticksCount4);

            ticksCount1 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(0).Id);
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            ticksCount2 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(1).Id);
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            ticksCount3 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(2).Id);
            ticksCount3 = DateTime.Now.Ticks - ticksCount3;

            ticksCount4 = DateTime.Now.Ticks;
            await instance.GetAsync(all.ElementAt(3).Id);
            ticksCount4 = DateTime.Now.Ticks - ticksCount4;

            Debug.WriteLine("Second execution: {0}, {1}, {2}, {3}", ticksCount1, ticksCount2, ticksCount3, ticksCount4);
            Debug.WriteLine("");
        }
    }
}
