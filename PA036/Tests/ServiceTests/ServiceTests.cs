using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Data;
using System;
using System.Diagnostics;

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
            _instance.GetAll();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            _instance.GetAll();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            Debug.WriteLine("First execution: {0}", ticksCount1);
            Debug.WriteLine("Second execution: {0}", ticksCount2);
        }
    }
}
