using CustomCollections;
using NUnit.Framework;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class CloneTests
    {
        [Test]
        public void Clone_NoArgs_AreEqual()
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three" };
            array[16] = 0;
            bool areEqual = true;

            DynamicArray<object> arrayCloned = array.Clone() as DynamicArray<object>;
            for (int i = 0; i < array.Capacity; i++)
            {
                if (!Equals(array[i], arrayCloned[i]))
                {
                    areEqual = false;
                    break;
                }
            }

            Assert.IsTrue(areEqual);
        }
    }
}
