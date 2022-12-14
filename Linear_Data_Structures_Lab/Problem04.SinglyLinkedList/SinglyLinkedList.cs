namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public T Element { get; set; }

            public Node Next { get; set; }

            public Node(T element)
            {
                this.Element = element;
            }

            public Node(T element, Node next)
            {
                Element = element;
                Next = next;
            }
        }

        private Node head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var node = this.head;
            this.head = new Node(item, node);
            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node(item);
            if (this.head == null)
            {
                this.head = newNode;
            }
            else
            {
                var node = this.head;
                while (node.Next != null)
                {
                    node = node.Next;
                }

                node.Next = newNode;
            }

            this.Count++;
        }

      

        public T GetFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            return this.head.Element;
        }

        public T GetLast()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            var node = this.head;
            while (node.Next != null)
            {
                node = node.Next;
            }

            return node.Element;
        }

        public T RemoveFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            var oldHead = this.head;
            this.head = oldHead.Next;
            this.Count--;
            return oldHead.Element;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            if (this.Count == 1)
            {
                this.Count--;
            }

            if (this.Count > 1)
            {
                var oldHead = this.head;
                while (oldHead.Next.Next != null)
                {
                    oldHead = oldHead.Next;
                }
                oldHead.Next = null;
                this.Count--;
            }

            return this.head.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = this.head;
            while (node != null)
            {
                yield return node.Element;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}