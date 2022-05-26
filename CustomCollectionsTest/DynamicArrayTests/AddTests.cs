using CustomCollections;
using NUnit.Framework;

namespace CustomCollectionsTest.DynamicArrayTests
{
    class AddTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(10)]
        public void Add_NotNullItem_AddedToEndWithExtention(object expected)
        {
            DynamicArray<object> array = new() { 0, 1, 2, "three", 4, "five", 6, "seven" };
            int count = array.Count;

            array.Add(expected);

            Assert.IsTrue(array.Contains(expected));
            Assert.AreEqual(count + 1, array.Count);
        }

        [TestCase(4)]
        public void Add_NotNullItem_AddedToCenter(object expected)
        {
            DynamicArray<object> array = new() { 0, 1 };
            array[3] = "three";
            int count = array.Count;
            object actual, actualNull;

            actualNull = array[2];
            array.Add(expected);
            actual = array[2];

            Assert.IsNull(actualNull);
            Assert.IsTrue(array.Contains(expected));
            Assert.AreSame(expected, actual);
            Assert.AreEqual(count + 1, array.Count);
        }
    }
}
