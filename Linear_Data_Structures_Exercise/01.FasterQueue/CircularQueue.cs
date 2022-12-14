﻿namespace Problem01.CircularQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CircularQueue<T> : IAbstractQueue<T>
    {
        private T[] elements;
        private int startIndex;
        private int endIndex;

        public CircularQueue(int capacity = 4)
        {
            this.elements = new T[capacity];
        }

        public int Count { get; set; }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var oldStartIndex = this.elements[this.startIndex];
            this.startIndex++;
            this.Count--;
            return oldStartIndex;
        }

        public void Enqueue(T item)
        {
            if (this.Count >= this.elements.Length) // case when startIndex == endIndex
            {
                this.Grow();
            }
            this.elements[this.endIndex] = item;
            this.endIndex = (this.endIndex + 1) % this.elements.Length;
            this.Count++;
        }

        private void Grow()
        {
            this.elements = this.CopyElements(new T[this.elements.Length * 2]);
            this.startIndex = 0;
            this.endIndex = this.Count;
        }

        private T[] CopyElements(T[] resultArr)
        {
            for (int currentIndex = 0; currentIndex < this.Count; currentIndex++)
            {
                resultArr[currentIndex] = this.elements[(this.startIndex + currentIndex) % this.elements.Length];
            }
            return resultArr;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.elements[this.startIndex];
        }

        public T[] ToArray()
        {
            return this.CopyElements(new T[this.Count]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int currentIndex = 0; currentIndex < this.Count; currentIndex++)
            {
                var index = (this.startIndex + currentIndex) % this.elements.Length;
                yield return this.elements[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
           return this.GetEnumerator();
        }
    }

}
