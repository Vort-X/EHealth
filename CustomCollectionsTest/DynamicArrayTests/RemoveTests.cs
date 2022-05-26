using CustomCollections;
using NUnit.Framework;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class RemoveTests
    {
        [TestCase(0)]
        [TestCase("five")]
        public void Remove_ContainedItem_Removed(object expected)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three", 4, "five", 6, "seven" };
            int count = array.Count;
            bool isContained, isRemoved;

            isContained = array.Contains(expected);
            isRemoved = array.Remove(expected);

            Assert.IsTrue(isContained);
            Assert.IsTrue(isRemoved);
            Assert.AreEqual(count - 1, array.Count);
        }

        [TestCase(3)]
        public void Remove_NotContainedItem_Removed(object expected)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three", 4, "five", 6, "seven" };
            int count = array.Count;
            bool isContained, isRemoved;

            isContained = array.Contains(expected);
            isRemoved = array.Remove(expected);

            Assert.IsFalse(isContained);
            Assert.IsTrue(!isRemoved && !array.Contains(expected));
            Assert.AreEqual(count, array.Count);
        }
    }
}
