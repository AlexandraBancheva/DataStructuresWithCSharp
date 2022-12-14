using System;
using System.Linq;
using _03.MinHeap;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            var priorityQueue = new OrderedBag<int>();
            priorityQueue.AddMany(cookies);

            int currentMinSweetness = priorityQueue[0];
            int steps = 0;

            while (currentMinSweetness < minSweetness && priorityQueue.Count > 1)
            {
                int leastSweetCookie = priorityQueue.RemoveFirst();
                int secondCookie = priorityQueue.RemoveFirst();

                int newCookie = leastSweetCookie + 2 * secondCookie;
                priorityQueue.Add(newCookie);
                currentMinSweetness = priorityQueue.GetFirst();
                steps++;
            }

            return currentMinSweetness < minSweetness ? -1 : steps;
        }
    }
}
