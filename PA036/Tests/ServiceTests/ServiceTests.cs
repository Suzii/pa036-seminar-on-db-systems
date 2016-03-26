using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.DataAccess;
using System;
using System.Diagnostics;

namespace Tests.ServiceTests
{
    [TestClass]
    public class ServiceTests
    {
        [TestMethod]
        public void GetData()
        {
            var ticksCount1 = DateTime.Now.Ticks;
            ProductService.GetAllProducts();
            ticksCount1 = DateTime.Now.Ticks - ticksCount1;

            var ticksCount2 = DateTime.Now.Ticks;
            ProductService.GetAllProducts();
            ticksCount2 = DateTime.Now.Ticks - ticksCount2;

            Debug.WriteLine("First execution: {0}", ticksCount1);
            Debug.WriteLine("Second execution: {0}", ticksCount2);
        }
    }
}
