using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollections
{
    public sealed class DynamicArray<T> : IList<T>, IReadOnlyCollection<T>, 
        ICollection<T>, IEnumerable<T>, IEnumerable, ICloneable where T : class
    {
        #region Fields
        private static readonly int initialCapacity = 8;

        private T[] vector;
        #endregion

        #region Constructors
        public DynamicArray()
        {
            vector = new T[initialCapacity];
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            if (collection is DynamicArray<T>)
            {
                vector = GetType().GetField(nameof(vector)).GetValue(collection) as T[];
            }
            else
            {
                vector = new T[initialCapacity];
                foreach (var item in collection)
                {
                    Add(item);
                }
            }
        }
        #endregion

        #region Indexers
        public T this[int index]
        {
            get
            {
                Check(index);
                if (index >= Capacity) return null;
                return vector[index];
            }
            set
            {
                Check(index);
                Extend(index);
                vector[index] = value;
                ItemAdded?.Invoke(this, new () { Index = index, Item = value });
            }
        }
        #endregion

        #region Properties
        public int Count
        {
            get
            {
                int count = 0;
                foreach (var item in this)
                {
                    count++;
                }
                return count;
            }
        }

        public bool IsReadOnly => false;

        public int Capacity => vector.Length;
        #endregion

        #region Events
        public event EventHandler Cleared;
        public event EventHandler<int> Extended;
        public event EventHandler<EventArgs> ItemAdded;
        public event EventHandler<EventArgs> ItemRemoved;
        #endregion

        #region Methods
        private static void Check(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
        }

        private static void Check(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
        }

        private void Extend(int includeIndex)
        {
            if (includeIndex < Capacity) return;
            var old = vector;

            var minArrayLength = includeIndex + 1;
            var minArrayPower = Math.Log2(minArrayLength);
            var newArrayPower = Math.Ceiling(minArrayPower);
            var newCapacity = (int)Math.Pow(2, newArrayPower);

            vector = new T[newCapacity];
            old.CopyTo(vector, 0);

            Extended?.Invoke(this, newCapacity);
        }

        public void Add(T item)
        {
            Check(item);
            for (int i = 0; i < Capacity; i++)
            {
                if (vector[i] is not null) continue;
                vector[i] = item;
                ItemAdded?.Invoke(this, new () { Index = i, Item = item });
                return;
            };
            Extend(Capacity);
            vector[Capacity] = item;
            ItemAdded?.Invoke(this, new () { Index = Capacity, Item = item });
        }

        public void Clear()
        {
            vector = new T[initialCapacity];
            Cleared?.Invoke(this, System.EventArgs.Empty);
        }

        public object Clone()
        {
            return new DynamicArray<T>(this);
        }

        public bool Contains(T item)
        {
            Check(item);
            foreach (var t in this)
            {
                if (Equals(t, item)) return true;
            };
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            vector.CopyTo(array, arrayIndex);
        }

        public IEnumerator GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            Check(item);
            for (int i = 0; i < Capacity; i++)
            {
                if (Equals(this[i], item)) return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            Check(item);
            Check(index);
            T current;
            T toInsert = item;
            int i = index;
            do
            {
                Extend(i);
                current = this[i];
                this[i] = toInsert;
                toInsert = current;
                i++;
            } while (current is not null);
            ItemAdded?.Invoke(this, new () { Index = index, Item = item });
        }

        public bool Remove(T item)
        {
            Check(item);
            for (int i = 0; i < Capacity; i++)
            {
                if (Equals(this[i], item))
                {
                    T toRemove = this[i];
                    this[i] = null;
                    ItemRemoved?.Invoke(this, new() { Index = i, Item = toRemove });
                    return true;
                }
            };
            return false;
        }

        public void RemoveAt(int index)
        {
            Check(index);
            if (index >= Capacity) return;
            T toRemove = this[index];
            this[index] = null;
            ItemRemoved?.Invoke(this, new() { Index = index, Item = toRemove });
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
            {
                T t = this[i];
                if (t is null) continue;
                yield return t;
            };
        }
        #endregion

        #region NestedClasses
        public class EventArgs
        {
            public int Index { get; init; }
            public T Item { get; init; }
        }
        #endregion
    }
}
