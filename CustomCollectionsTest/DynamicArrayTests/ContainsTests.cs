using CustomCollections;
using NUnit.Framework;
using System;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class ContainsTests
    {
        private DynamicArray<object> array;

        [SetUp]
        public void Setup()
        {
            array = new(new object[] { 0, 1, 2, "three" });
        }

        [TestCase(0)]
        public void Contains_ContainedValue_ReturnsTrue(object expected)
        {
            bool isContained;

            isContained = array.Contains(expected);

            Assert.IsTrue(isContained);
        }

        [TestCase(3)]
        public void Contains_NotContainedValue_ReturnsFalse(object expected)
        {
            bool isContained;

            isContained = array.Contains(expected);

            Assert.IsFalse(isContained);
        }
    }
}
