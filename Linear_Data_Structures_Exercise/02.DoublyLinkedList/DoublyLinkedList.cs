namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public Node  Next { get; set; }
            public Node  Previous { get; set; }
            public T Value{ get; set; }

            public Node(T value)
            {
                this.Value = value;
            }
        }

        private Node head;
        private Node tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newNode = new Node(item);
            if (this.Count == 0)
            {
                this.head = newNode;
                this.tail = newNode;

                //OR
                // this.head = this.tail = newNode;
            }
            else
            {
                this.head.Previous = newNode;
                newNode.Next = this.head;
                this.head = newNode;
            }
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);
            if (this.head == null)
            {
                this.head = this.tail = newNode;
            }
            else
            {
                newNode.Previous = this.tail;
                this.tail.Next = newNode;
                this.tail = newNode;
            }
            this.Count++;
        }

        public T GetFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this.head.Value;
        }

        public T GetLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this.tail.Value;
        }

        public T RemoveFirst()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var currentHead = this.head;

            if (this.head.Next == null)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                var newHead = this.head.Next;
                newHead.Previous = null;
                this.head = newHead;
            }
            this.Count--;
            return currentHead.Value;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            var currentTail = this.tail;
            if (this.tail.Previous == null)
            {
                this.head = null;
                this.tail = null;
            }
            else
            {
                var newTail = this.tail.Previous;
                newTail.Next = null;
                this.tail = newTail;
            }
            this.Count--;
            return currentTail.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}