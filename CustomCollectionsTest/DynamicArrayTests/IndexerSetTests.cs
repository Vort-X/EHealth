using CustomCollections;
using NUnit.Framework;
using System;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class IndexerSetTests
    {
        [TestCase(1, "one")]
        [TestCase(3, 3)]
        [TestCase(32, "misha moment")]
        public void IndexerSet_PositiveIndexes_SetsItem(int index, object expected)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three" };

            array[index] = expected;

            Assert.IsTrue(array.Contains(expected));
            Assert.AreSame(expected, array[index]);
        }

        [TestCase(-1, "anything")]
        [TestCase(-32, 42)]
        public void IndexerSet_NegativeIndexes_ThrowsException(int index, object item)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three" };
            TestDelegate code;

            code = () => array[index] = item;

            Assert.Throws<ArgumentOutOfRangeException>(code);
        }
    }
}
