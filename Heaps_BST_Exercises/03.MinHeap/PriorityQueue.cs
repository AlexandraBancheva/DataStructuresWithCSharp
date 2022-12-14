using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {
        private Dictionary<T, int> indexes;
        public PriorityQueue()
        {
            this.indexes = new Dictionary<T, int>();
            this.elements = new List<T>();
        }

        public void Enqueue(T element)
        {
            this.elements.Add(element);
            this.indexes.Add(element, this.Count - 1);
            this.HeapifyUp(this.Count - 1);
        }

        public T Dequeue()
        {
            this.ValidateIfEmpty();

            T result = this.elements[0];
            this.Swap(0, this.Count - 1);
            this.elements.RemoveAt(this.Count - 1);
            this.indexes.Remove(result);

            this.HeapifyDown(0);

            return result;
        }

        private void HeapifyDown(int index)
        {
            var smallest = index;

            var left = this.GetLeft(index);
            var right = this.GetRight(index);

            if (left < this.Count && this.elements[left].CompareTo(this.elements[index]) < 0)
            {
                smallest = left;
            }
            if (right < this.Count && this.elements[right].CompareTo(this.elements[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                var temp = this.elements[index];
                this.elements[index] = this.elements[smallest];
                this.elements[smallest] = temp;

                this.HeapifyDown(smallest);
            }
        }

        private int GetRight(int index)
        {
            return 2 * index + 2;
        }

        private int GetLeft(int index)
        {
            return 2 * index + 1;
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = temp;

            this.indexes[this.elements[index]] = index;
            this.indexes[this.elements[parentIndex]] = parentIndex;
        }

        private bool ValidateIfEmpty()
        {
            if (this.elements[0] != null )
            {
                return true;
            }
            return false;
        }

        public void DecreaseKey(T key)
        {
            // Неоптимално решение;
            var index = this.elements.IndexOf(key);
            this.HeapifyUp(index);

            //this.HeapifyUp(this.indexes[key]);
        }

        //public void DecreaseKey(T key, T newKey) // If we work with strings or integers
        //{
        //    var oldIndex = this.indexes[key];
        //    this.elements[oldIndex] = newKey;
        //    this.indexes.Remove(key);
        //    this.indexes.Add(newKey, oldIndex);

        //    this.HeapifyUp(oldIndex);
        //}

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParent(index);
            if (parentIndex >= 0 && this.elements[index].CompareTo(this.elements[parentIndex]) < 0)
            {
                var temp = this.elements[index];
                this.elements[index] = this.elements[parentIndex];
                this.elements[parentIndex] = temp;

                this.HeapifyUp(parentIndex);
            }
        }

        private int GetParent(int index)
        {
            if (index <= 0)
            {
                return -1;
            }
            return (index - 1) / 2;
        }
    }
}
