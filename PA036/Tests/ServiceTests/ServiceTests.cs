using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Config;
using Service.Data;
using Service.DTO;
using Shared.Enums;
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
        private IStoreService _storeInstanceWithContext2;
        private IStoreService _storeInstanceWithContext3;
        private IDatabaseService _database;

        [TestInitialize]
        public void BeforeMethod()
        {
            _database = new DatabaseService();
            _instanceWithContext1 = new ProductService(new DbSettings() { AppContext = AppContexts.Local });
            _instanceWithContext2 = new ProductService(new DbSettings() { AppContext = AppContexts.Azure });

            _storeInstanceWithContext2 = new StoreService(new DbSettings() { AppContext = AppContexts.Azure });
            _storeInstanceWithContext3 = new StoreService(new DbSettings() { AppContext = AppContexts.LocalNamedAsAzure });
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

        [TestMethod]
        public async Task TestAzureVersusLocalWithSameName()
        {
            // Prevent to run this on automaded build service or db that is not supposed to here
            if ((await _storeInstanceWithContext2.TotalCountAsync()) == 0)
                return;

            var storeFilter = new StoreFilter() { NameFilter = "Amazing test" };
            var storeResultCleanUp1 = (await _storeInstanceWithContext2.GetAsync(storeFilter));
            _database.InvalidateCache();
            var storeResultCleanUp2 = (await _storeInstanceWithContext3.GetAsync(storeFilter));

            foreach (var item in storeResultCleanUp1)
                await _storeInstanceWithContext2.DeleteAsync(item.Id);

            _database.InvalidateCache();
            foreach (var item in storeResultCleanUp2)
                await _storeInstanceWithContext3.DeleteAsync(item.Id);

            var store1 = new StoreDTO() { Name = "Amazing test",  City = "Brno", State = "Czech Republic" };
            var store2 = new StoreDTO() { Name = "Amazing test", City = "Zilina", State = "Slovakia" };

            await _storeInstanceWithContext2.CreateAsync(store1);
            await _storeInstanceWithContext3.CreateAsync(store2);

            var storeResult1 =  (await _storeInstanceWithContext2.GetAsync(storeFilter)).Single();
            var storeResult2 = (await _storeInstanceWithContext3.GetAsync(storeFilter)).Single();

            Assert.AreEqual("Amazing test", storeResult1.Name);
            Assert.AreEqual("Amazing test", storeResult2.Name);

            Assert.AreEqual("Brno", storeResult1.City);
            Assert.AreEqual("Brno", storeResult2.City);

            // cleanup
            await _storeInstanceWithContext2.DeleteAsync(storeResult1.Id);
            _database.InvalidateCache();
            storeResult2 = (await _storeInstanceWithContext3.GetAsync(storeFilter)).Single();
            await _storeInstanceWithContext3.DeleteAsync(storeResult2.Id);
        }
    }
}
