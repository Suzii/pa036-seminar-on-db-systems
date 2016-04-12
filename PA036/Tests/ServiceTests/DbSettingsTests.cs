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
            _database.InvalidateCache();

            var modifier = new ProductFilter();
            modifier.Take = 4;
            await _products.GetAsync(modifier);
            await _products.GetAsync(modifier, new DbSettings() { UseSecondAppContext = true });

            var count = _database.GetCacheItemsCount();
            _database.InvalidateCache();
            var countAfterInvalidate = _database.GetCacheItemsCount();
            // Depends on if we have 2 different databases or not
            Assert.IsTrue(countAfterInvalidate == count - 1 || countAfterInvalidate == count - 2); 
        }
    }
}
