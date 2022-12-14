namespace _02.BinarySearchTree
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;

    public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Count { get; set; }
        }

        private Node root;

        private BinarySearchTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public BinarySearchTree()
        {
        }

        public void Insert(T element)
        {
            this.root = this.Insert(element, this.root);
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new BinarySearchTree<T>(current);
        }

        // One test doesn't work!
        public void Delete(T element)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            var current = this.FindElement(element);

            if (this.root == current)
            {
                this.root = null;
            }
            else
            {
                this.root = this.Delete(this.root, element);
            }

        }

        private Node Delete(Node node, T element)
        {
            if (node.Value.CompareTo(element) > 0)
            {
                node.Left = this.Delete(node.Left, element);
            }
            else if (node.Value.CompareTo(element) < 0)
            {
                node.Right = this.Delete(node.Right, element);
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    //node = this.MinValue(node.Right);
                    //node.Right = this.Delete(node.Right, node.Value);
                    return node.Left;
                }

                //var res = this.MinValue(node.Right);
            }

            return node;
        }

        //private Node MinValue(Node node)
        //{
        //    var minValue = node;//.Value;
        //    while (node.Left != null)
        //    {
        //        minValue = node.Left;
        //        node = node.Left;
        //    }

        //    return minValue;
        //}

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMax(this.root);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            node.Right = this.DeleteMax(node.Right);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
            return node;
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = this.DeleteMin(node.Left);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right); // It's necessary in DeleteMax and Delete;
            return node;
        }

        public int Count()
        {
           return this.Count(this.root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            //return 1 + this.Count(node.Left) + this.Count(node.Right);
            return node.Count;
        }

        public int Rank(T element)
        {
            return this.Rank(element, this.root);
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                return this.Rank(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
            }

            return this.Count(node.Left);
        }

        public T Select(int rank)
        {
            Node node = this.Select(this.root, rank);
            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        private Node Select(Node node, int rank)
        {
            if (node == null)
            {
                return node;
            }

            int leftCount = this.Count(node.Left);

            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return this.Select(node.Left, rank);
            }
            else
            {
                return this.Select(node.Right, rank - (leftCount + 1));
            }

        }

        public T Ceiling(T element)
        {
            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            var collection = new Queue<T>(); // It's possible to used List.
            this.Range(this.root, startRange, endRange, collection);
            return collection;
        }

        private void Range(Node node, T startRange, T endRange, Queue<T> queue)
        {
            if (node == null)
            {
                return;
            }

            bool nodeInLowerRange = startRange.CompareTo(node.Value) < 0;
            bool nodeInUpperRange = endRange.CompareTo(node.Value) > 0;

            if (nodeInLowerRange)
            {
                this.Range(node.Left, startRange, endRange, queue);
            }

            if (startRange.CompareTo(node.Value) <= 0 && endRange.CompareTo(node.Value) >= 0)
            {
                queue.Enqueue(node.Value);
            }

            if (nodeInUpperRange)
            {
                this.Range(node.Right, startRange, endRange, queue);
            }
        }

        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = this.Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = this.Insert(element, node.Right);
            }

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
    }
}
