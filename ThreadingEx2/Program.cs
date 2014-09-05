using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FindSmallest
{
    class Program
    {

        private static readonly int[][] Data = new int[][]{
            new[]{1,5,4,2}, 
            new[]{3,2,4,11,4},
            new[]{33,2,3,-1, 10},
            new[]{3,2,8,9,-1},
            new[]{1, 22,1,9,-3, 5}
        };

        

        private static int FindSmallest(int[] numbers)
        {
            if (numbers.Length < 1)
            {
                throw new ArgumentException("There must be at least one element in the array");
            }

            int smallestSoFar = numbers[0];
            foreach (int number in numbers)
            {
                if (number < smallestSoFar)
                {
                    smallestSoFar = number;
                }
            }
            
            return smallestSoFar;
        }

        static void Main()
        {
            //Task<int> enables the use of Task.Result
            //allTasks hold all tasks that will be/has been run
            List<Task<int>> allTasks = new List<Task<int>>();

            foreach (int[] d in Data)
            {
                Task<int> task = new Task<int>(() =>
                {
                    int smallestSoFar = FindSmallest(d);
                    
                    Console.WriteLine("\t" + String.Join(", ", d) + "\n -> " + smallestSoFar);

                    //Task<int> needs a return value
                    return smallestSoFar;
                });
                allTasks.Add(task);
                task.Start();
            }

            Console.WriteLine("\nSmallest of all arrays: \n");

            //A list to hold all of the smallest ints from each array
            List<int> smallestOfAllInts = new List<int>();

            foreach (var task in allTasks)
            {
                Console.WriteLine(task.Result + " ");
                smallestOfAllInts.Add(task.Result);
            }
            Console.WriteLine("Smallest of all: " + FindSmallest(smallestOfAllInts.ToArray()));

        }
    }
}
