namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class IntegerTree : Tree<int>, IIntegerTree
    {
        public IntegerTree(int key, params Tree<int>[] children)
            : base(key, children)
        {
        }

        // DFS
        public IEnumerable<IEnumerable<int>> GetPathsWithGivenSum(int sum)
        {
            var result = new List<List<int>>();
            var currentPath = new LinkedList<int>();
            currentPath.AddFirst(this.Key);

            int currentSum = this.Key;
            this.Dfs(this, result, currentPath, ref currentSum, sum);
            return result;
        }

        private void Dfs(Tree<int> subtree,
                         List<List<int>> result, 
                         LinkedList<int> currentPath, 
                         ref int currentSum, 
                         int wantedSum)
        {
            foreach (var child in subtree.children)
            {
                currentSum += child.Key;
                currentPath.AddLast(child.Key);
                this.Dfs(child, result, currentPath, ref currentSum, wantedSum);
            }

            if (currentSum == wantedSum)
            {
                result.Add(new List<int>(currentPath));
            }

            currentSum -= subtree.Key;
            currentPath.RemoveLast();
        }

        // BFS
        public IEnumerable<Tree<int>> GetSubtreesWithGivenSum(int sum)
        {
            var result = new List<Tree<int>>();
            var allSubtrees = this.GetAllNodesByBfs();

            foreach (var subtree in allSubtrees)
            {
                if (this.HasGivenSum(subtree, sum))
                {
                    result.Add(subtree);
                }
            }

            return result;
        }

        private bool HasGivenSum(Tree<int> subtree, int wantedSum)
        {
            var actualSum = subtree.Key;
            this.GetSubtreeSumByDfs(subtree, wantedSum, ref actualSum);

            return actualSum == wantedSum;
        }

        private void GetSubtreeSumByDfs(Tree<int> subtree, int wantedSum, ref int actualSum)
        {
            foreach (var child in subtree.Children)
            {
                actualSum += child.Key;
                this.GetSubtreeSumByDfs(child, wantedSum, ref actualSum);
            }
        }

        private List<Tree<int>> GetAllNodesByBfs()
        {
            var result = new List<Tree<int>>();
            var queue = new Queue<Tree<int>>();

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }
    }
}
