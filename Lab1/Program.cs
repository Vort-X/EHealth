using CustomCollections;
using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new DynamicArray<object>();

            array.Cleared += OnCleared;
            array.Extended += OnExtended;
            array.ItemAdded += OnItemAdded;
            array.ItemRemoved += OnItemRemoved;

            array.Add(10);
            array.Add(20);
            array.Add(30);

            array[15] = 160;

            var val1 = 30;
            var val2 = 40;
            Console.WriteLine($"Array contains value {{{val1}}}: {array.Contains(val1)}");
            Console.WriteLine($"Array contains value {{{val2}}}: {array.Contains(val2)}");

            array.RemoveAt(1);

            array.Clear();

            array[1024] = "Spaghetti";
        }

        static void OnCleared(object sender, EventArgs e)
        {
            Console.WriteLine($"Array cleared");
        }

        static void OnExtended(object sender, int e)
        {
            Console.WriteLine($"Array({(sender as DynamicArray<object>).Count}) has extended to new capacity {{{e}}}");
        }

        static void OnItemAdded(object sender, DynamicArray<object>.EventArgs e)
        {
            Console.WriteLine($"Array({(sender as DynamicArray<object>).Count}) has new item {{{e.Item}}} on index {{{e.Index}}}");
        }

        static void OnItemRemoved(object sender, DynamicArray<object>.EventArgs e)
        {
            Console.WriteLine($"Array({(sender as DynamicArray<object>).Count})'s value {{{e.Item}}} removed on index {{{e.Index}}}");
        }
    }
}
