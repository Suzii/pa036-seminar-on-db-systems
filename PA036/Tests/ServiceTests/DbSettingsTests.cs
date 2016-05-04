using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Config;
using Service.Data;
using Shared.Enums;
using Shared.Filters;
using Shared.Settings;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    [TestClass]
    public class DbSettingsTests
    {
        private IDatabaseService _database;
        private IProductService _productsServiceWithContext2;

        [TestInitialize]
        public void BeforeMethod()
        {
            _database = new DatabaseService();
            _productsServiceWithContext2 = new ProductService(new DbSettings() { AppContext = AppContexts.Azure });
        }

        [TestMethod]
        public async Task InvalidateCacheTest()
        {
            var modifier = new ProductFilter();
            modifier.Take = 4;
            await _productsServiceWithContext2.GetAsync(modifier);
            await _productsServiceWithContext2.GetAsync(modifier);

            _database.InvalidateCache();
            Assert.AreEqual(0, _database.GetCacheItemsCount()); 
        }
    }
}
