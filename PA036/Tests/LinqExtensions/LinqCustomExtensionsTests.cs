using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Shared.Extensions;

namespace Tests.LinqExtensions
{
    [TestClass]
    public class LinqCustomExtensionsTests
    {
        [TestMethod]
        public void ZippedAverage_ResultLengthTest()
        {
            var list1 = new List<double>() { 1, 2, 3, 4, 5 };
            var list2 = new List<double>() { 1, 2 };
            var list3 = new List<double>() { 1, 2, 3, 4 };

            var composedLists = new List<List<double>>() { list1, list2, list3 };

            var result = composedLists.ZippedAverage();

            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void ZippedAverage_AveragesTest1()
        {
            var list1 = new List<double>() {1, 2, 3, 4};
            var list2 = new List<double>() {1, 2, 3, 4};
            var list3 = new List<double>() {1, 2, 3, 4};

            var composedLists = new List<List<double>>() {list1, list2, list3};

            var result = composedLists.ZippedAverage().ToList();

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
            Assert.AreEqual(3, result[2]);
            Assert.AreEqual(4, result[3]);
        }

        [TestMethod]
        public void ZippedAverage_AveragesTest2()
        {
            var list1 = new List<double>() { 1, 2, 5, 7 };
            var list2 = new List<double>() { 2, 3, 4, 5 };
            var list3 = new List<double>() { 3, 4, 3, 6 };

            var composedLists = new List<List<double>>() { list1, list2, list3 };

            var result = composedLists.ZippedAverage().ToList();

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(2, result[0]);
            Assert.AreEqual(3, result[1]);
            Assert.AreEqual(4, result[2]);
            Assert.AreEqual(6, result[3]);
        }
    }
}
