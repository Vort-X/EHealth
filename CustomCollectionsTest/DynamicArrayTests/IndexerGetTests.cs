using CustomCollections;
using NUnit.Framework;
using System;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class IndexerGetTests
    {
        private DynamicArray<object> array;

        [SetUp]
        public void Setup()
        {
            array = new(new object[] { 0, 1, 2, "three" });
        }

        [TestCase(1, 1)]
        [TestCase(3, "three")]
        public void IndexerGet_PositiveIndexes_ReturnItems(int index, object expected)
        {
            object actual;

            actual = array[index];

            Assert.AreEqual(expected, actual);
        }

        [TestCase(7)]
        [TestCase(32)]
        public void IndexerGet_PositiveIndexes_ReturnNulls(int index)
        {
            object actual;

            actual = array[index];

            Assert.IsNull(actual);
        }

        [TestCase(-1)]
        [TestCase(-32)]
        public void IndexerGet_NegativeIndexes_ThrowsException(int index)
        {
            TestDelegate code;

            code = () => { var _ = array[index]; };

            Assert.Throws<ArgumentOutOfRangeException>(code);
        }
    }
}
