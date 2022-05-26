using CustomCollections;
using NUnit.Framework;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class ClearTests
    {
        [Test]
        public void Clear_NoArgs_Cleared()
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three" };

            array.Clear();

            Assert.AreEqual(0, array.Count);
            Assert.AreEqual(8, array.Capacity);
        }
    }
}
