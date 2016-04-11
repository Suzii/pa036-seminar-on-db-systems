using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Data;
using Shared.Filters;
using Shared.Settings;
using System;
using System.Diagnostics;
using System.Linq;

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
        public void GetData()
        {
            var ticksCount1 = DateTime.Now.Ticks;
            _instance.GetAllAsync().Wait();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            _instance.GetAllAsync().Wait();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            Debug.WriteLine("First execution: {0}", ticksCount1);
            Debug.WriteLine("Second execution: {0}", ticksCount2);
        }

        [TestMethod]
        public void TotalCount()
        {
            var count = _instance.TotalCountAsync().Result;
            Debug.WriteLine("Total count: {0}", count);
        }
        
        [TestMethod]
        public void DatabaseNotEmpty()
        {
            var product = _instance.GetAllAsync().Result.FirstOrDefault();
            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void Context1()
        {
            var settings = new DbSettings() { UseSecondAppContext = false };
            TestGet(settings);
        }

        [TestMethod]
        public void Context2()
        {
            var settings = new DbSettings() { UseSecondAppContext = true };
            TestGet(settings);
        }

        private void TestGet(DbSettings settings)
        {
            Debug.WriteLine("Use second app context: {0}", settings.UseSecondAppContext);

            var modifier = new ProductFilter();
            modifier.Take = 4;
            var all = _instance.GetAsync(modifier, settings).Result;

            var ticksCount1 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(0).Id, settings).Wait();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(1).Id, settings).Wait();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            var ticksCount3 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(2).Id, settings).Wait();
            ticksCount3 = DateTime.Now.Ticks - ticksCount3;

            var ticksCount4 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(3).Id, settings).Wait();
            ticksCount4 = DateTime.Now.Ticks - ticksCount4;

            Debug.WriteLine("First execution: {0}, {1}, {2}, {3}", ticksCount1, ticksCount2, ticksCount3, ticksCount4);

            ticksCount1 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(0).Id, settings).Wait();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            ticksCount2 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(1).Id, settings).Wait();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            ticksCount3 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(2).Id, settings).Wait();
            ticksCount3 = DateTime.Now.Ticks - ticksCount3;

            ticksCount4 = DateTime.Now.Ticks;
            _instance.GetAsync(all.ElementAt(3).Id, settings).Wait();
            ticksCount4 = DateTime.Now.Ticks - ticksCount4;

            Debug.WriteLine("Second execution: {0}, {1}, {2}, {3}", ticksCount1, ticksCount2, ticksCount3, ticksCount4);
            Debug.WriteLine("");
        }
    }
}
