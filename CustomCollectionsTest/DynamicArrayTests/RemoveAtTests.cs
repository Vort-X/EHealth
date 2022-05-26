using CustomCollections;
using NUnit.Framework;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class RemoveAtTests
    {
        [TestCase(0)]
        public void RemoveAt_InRangeIndex_Removed(int index)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three", 4, "five", 6, "seven" };
            int count = array.Count;

            array.RemoveAt(index);

            Assert.IsNull(array[index]);
            Assert.AreEqual(count - 1, array.Count);
        }

        [TestCase(16)]
        public void RemoveAt_OutOfRangeIndex_NotRemoved(int index)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three", 4, "five", 6, "seven" };
            int count = array.Count;

            array.RemoveAt(index);

            Assert.IsNull(array[index]);
            Assert.AreEqual(count, array.Count);
        }
    }
}
