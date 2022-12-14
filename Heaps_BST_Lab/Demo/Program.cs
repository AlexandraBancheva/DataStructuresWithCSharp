using System;
using System.Diagnostics.CodeAnalysis;
using _02.BinarySearchTree;
using _03.MaxHeap;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var tree = new BinarySearchTree<int>();

            //tree.Insert(8);
            //tree.Insert(4);
            //tree.Insert(2);
            //tree.Insert(6);

            //tree.EachInOrder(Console.WriteLine);

            //var newTree = tree.Search(6);
            //newTree.Insert(9);

            ////  Console.WriteLine(tree.Contains(4));
            ////Console.WriteLine(tree.Contains(11));

            //newTree.EachInOrder(Console.WriteLine);
            //tree.EachInOrder(Console.WriteLine);

            var heap = new MaxHeap<int>();
            heap.Add(4);
            heap.Add(7);
            heap.Add(11);
            heap.Add(18);
            heap.Add(2);
            heap.Add(5);
            heap.Add(8);
            heap.Add(1);
            heap.Add(21);

            Console.WriteLine(heap.ExtractMax());
        }
    }

    //class Test : IComparable<Test>
    //{
    //    private int a;

    //    public Test(int a)
    //    {
    //        this.a = a; 
    //    }

    //    public int CompareTo([AllowNull] Test obj)
    //    {
    //        return this.a - obj.a;
    //    }

    //    public override string ToString()
    //    {
    //        return a.ToString();
    //    }
    //}
}