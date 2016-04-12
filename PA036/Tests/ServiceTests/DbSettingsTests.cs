using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Config;
using Service.Data;
using Shared.Filters;
using Shared.Settings;
using System.Threading.Tasks;

namespace Tests.ServiceTests
{
    [TestClass]
    public class DbSettingsTests
    {
        private IDatabaseService _database;
        private IProductService _products;

        [TestInitialize]
        public void BeforeMethod()
        {
            _database = new DatabaseService();
            _products = new ProductService();
        }

        [TestMethod]
        public async Task InvalidateCacheTest()
        {
            var modifier = new ProductFilter();
            modifier.Take = 4;
            await _products.GetAsync(modifier);
            await _products.GetAsync(modifier, new DbSettings() { UseSecondAppContext = true });

            _database.InvalidateCache();
            Assert.AreEqual(0, _database.GetCacheItemsCount()); 
        }
    }
}
