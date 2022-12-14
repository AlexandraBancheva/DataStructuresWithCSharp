using System;
using System.Collections.Generic;
using System.Text;

namespace _03.MinHeap
{
    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        protected List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Count => this.elements.Count;

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp(this.elements.Count - 1);
        }

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

        public T ExtractMin()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T item = this.elements[0];
            this.elements[0] = this.elements[this.elements.Count - 1];
            this.elements.RemoveAt(this.elements.Count - 1);

            this.HeapifyDown(0);
            return item;
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

        public T Peek()
        {
            if (this.elements.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.elements[0];
        }
    }
}
