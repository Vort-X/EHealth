using CustomCollections;
using NUnit.Framework;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class InsertTests
    {
        [TestCase(4, 4)]
        [TestCase(7, 0)]
        public void Insert_ValidInput_Inserted(int index, object item)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three" };
            array[6] = 6;
            array[7] = "seven";

            array.Insert(index, item);

            Assert.IsTrue(array.Contains(item));
            Assert.AreEqual(item, array[index]);
        }

        [TestCase(0)]
        public void Insert_NotNullItem_InsertedAndShifted(object item)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three" };
            array[6] = 6;
            array[7] = "seven";

            array.Insert(6, item);

            Assert.IsTrue(array.Contains(item));
            Assert.AreEqual(item, array[6]);
            Assert.AreEqual(6, array[7]);
            Assert.AreEqual("seven", array[8]);
        }
    }
}
