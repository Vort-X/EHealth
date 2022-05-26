using CustomCollections;
using NUnit.Framework;
using System;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class IndexOfTests
    {
        private DynamicArray<object> array;

        [SetUp]
        public void Setup()
        {
            array = new() { 0, 1, 2, "three" };
            array[7] = "seven";
        }

        [TestCase(0, 0)]
        [TestCase("seven", 7)]
        public void IndexOf_ContainedValue_ReturnsIndex(object item, int expected)
        {
            int actual;

            actual = array.IndexOf(item);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(3)]
        public void IndexOf_NotContainedValue_ReturnsNegativeOne(object item)
        {
            int actual;

            actual = array.IndexOf(item);

            Assert.AreEqual(-1, actual);
        }
    }
}
