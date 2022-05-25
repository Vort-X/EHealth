using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomCollections
{
    public class LinkedList2<T> : IReadOnlyCollection<T>, ICollection<T>, IEnumerable<T>, IEnumerable, ICloneable
    {
        public LinkedList2()
        {
            
        }
        public LinkedList2(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                AddLast(item);
            }
        }

        public LinkedListNode2<T>? Last { get; internal set; }
        public LinkedListNode2<T>? First { get; internal set; }
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

        bool ICollection<T>.IsReadOnly => false;

        public event EventHandler ListCleared;
        public event EventHandler<T> NodeFirstUpdated;
        public event EventHandler<T> NodeLastUpdated;
        public event EventHandler<T> ValueAdded;
        public event EventHandler<T> ValueRemoved;

        public void AddAfter(LinkedListNode2<T> node, LinkedListNode2<T> newNode)
        {
            if (node.List != this)
            {
                throw new ArgumentException("Node does not belong to list", nameof(node));
            }

            if (newNode.List == this)
            {
                throw new ArgumentException("New node belongs to list", nameof(newNode));
            }

            var next = node.Next;
            if (next == null)
            {
                AddLast(newNode);
                return;
            }
            node.Next = newNode;
            next.Previous = newNode;

            newNode.Previous = node;
            newNode.Next = next;
            newNode.List = this;

            ValueAdded?.Invoke(this, newNode.Value);
        }
        public LinkedListNode2<T> AddAfter(LinkedListNode2<T> node, T value)
        {
            var newNode = new LinkedListNode2<T>(value);
            AddAfter(node, newNode);
            return newNode;
        }
        public void AddBefore(LinkedListNode2<T> node, LinkedListNode2<T> newNode)
        {
            if (node.List != this)
            {
                throw new ArgumentException("Node does not belong to list", nameof(node));
            }

            if (newNode.List == this)
            {
                throw new ArgumentException("New node belongs to list", nameof(newNode));
            }

            var prev = node.Previous;
            if (prev == null)
            {
                AddFirst(newNode);
                return;
            }
            node.Previous = newNode;
            prev.Next = newNode;

            newNode.Previous = prev;
            newNode.Next = node;
            newNode.List = this;

            ValueAdded?.Invoke(this, newNode.Value);
        }
        public LinkedListNode2<T> AddBefore(LinkedListNode2<T> node, T value)
        {
            var newNode = new LinkedListNode2<T>(value);
            AddBefore(node, newNode);
            return newNode;
        }
        public void AddFirst(LinkedListNode2<T> node)
        {
            if (node.List == this)
            {
                throw new ArgumentException("New node belongs to list", nameof(node));
            }

            node.List = this;
            if (Count == 0)
            {
                Last = node;
            }
            else
            {
                node.Next = First;
                node.Previous = null;
                node.List = this;
                First.Previous = node;
            }
            First = node;

            ValueAdded?.Invoke(this, node.Value);
            NodeFirstUpdated?.Invoke(this, node.Value);
            if (Count == 1)
            {
                NodeLastUpdated?.Invoke(this, node.Value);
            }
        }
        public LinkedListNode2<T> AddFirst(T value)
        {
            var newNode = new LinkedListNode2<T>(value);
            AddFirst(newNode);
            return newNode;
        }
        public void AddLast(LinkedListNode2<T> node)
        {
            if (node.List == this)
            {
                throw new ArgumentException("New node belongs to list", nameof(node));
            }

            node.List = this;
            if (Count == 0)
            {
                First = node;
                Last = node;
            }
            else
            {
                node.Next = null;
                node.Previous = Last;
                node.List = this;
                Last.Next = node;
            }
            Last = node;

            ValueAdded?.Invoke(this, node.Value);
            NodeLastUpdated?.Invoke(this, node.Value);
            if (Count == 1)
            {
                NodeFirstUpdated?.Invoke(this, node.Value);
            }
        }
        public LinkedListNode2<T> AddLast(T value)
        {
            var newNode = new LinkedListNode2<T>(value);
            AddLast(newNode);
            return newNode;
        }
        public void Clear()
        {
            Last = null;
            First = null;

            ListCleared?.Invoke(this, EventArgs.Empty);
        }
        public object Clone()
        {
            return new LinkedList2<T>(this);
        }
        public bool Contains(T value)
        {
            foreach (var item in this)
            {
                if (Equals(item, value)) return true;
            }
            return false;
        }
        public void CopyTo(T[] array, int index)
        {
            var current = First;
            for (; index < array.Length; index++)
            {
                if (current == null) return;
                array[index] = current.Value;
                current = current.Next;
            }
        }
        public LinkedListNode2<T>? Find(T value)
        {
            var current = First;
            while (current != null)
            {
                if (Equals(current.Value, value))
                {
                    return current;
                }
                current = current.Next;
            }
            return null;
        }
        public LinkedListNode2<T>? FindLast(T value)
        {
            var current = Last;
            while (current != null)
            {
                if (Equals(current.Value, value))
                {
                    return current;
                }
                current = current.Previous;
            }
            return null;
        }
        public void Remove(LinkedListNode2<T> node)
        {
            if (node.List != this)
            {
                throw new ArgumentException("Node does not belong to list", nameof(node));
            }

            node.List = null;

            if (Count == 1)
            {
                First = null;
                Last = null;

                ListCleared?.Invoke(this, EventArgs.Empty);
                ValueRemoved?.Invoke(this, node.Value);
                return;
            }

            if (Equals(node, First))
            {
                First = node.Next;
            }
            else if (Equals(node, Last))
            {
                Last = node.Previous;
            }
            else
            {
                var next = node.Next;
                var prev = node.Previous;
                next.Previous = prev;
                prev.Next = next;
            }

            ValueRemoved?.Invoke(this, node.Value);
        }
        public bool Remove(T value)
        {
            var node = Find(value);
            if (node == null) return false;
            Remove(node);
            return true;
        }
        public void RemoveFirst()
        {
            if (First == null)
            {
                return;
            }
            var node = First;
            First.List = null;
            if (First.Next == null)
            {
                First = null;
                Last = null;

                ListCleared?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                First = First.Next;
            }

            ValueRemoved?.Invoke(this, node.Value);
        }
        public void RemoveLast()
        {
            if (First == null)
            {
                return;
            }
            var node = Last;
            Last.List = null;
            if (Last.Previous == null)
            {
                First = null;
                Last = null;

                ListCleared?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Last = Last.Previous;
            }

            ValueRemoved?.Invoke(this, node.Value);
        }

        void ICollection<T>.Add(T item)
        {
            AddLast(item);
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }

    public sealed class LinkedListNode2<T>
    {
        private T value;

        public LinkedListNode2(T value)
        {
            Value = value;
        }
        public LinkedList2<T>? List { get; internal set; }
        public LinkedListNode2<T>? Next { get; internal set; }
        public LinkedListNode2<T>? Previous { get; internal set; }
        public T Value { get => value; set => this.value = value; }
        public ref T ValueRef { get => ref value; }
    }
}
