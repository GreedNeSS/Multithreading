using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgrammingWithAsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Dictionary<string, Task<int>> tasks = new Dictionary<string, Task<int>>();
            for (int i = 5; i < 10; i++)
            {
                tasks.Add($"Task #{i - 4}", SquareAsync(i));
            }

            int[] results = await Task.WhenAll<int>(tasks.Values);

            Console.WriteLine("***** Array of results *****\n");

            foreach (int resNum in results)
            {
                Console.WriteLine(resNum);
            }

            Console.WriteLine("\n***** Task information *****\n");

            foreach (var key in tasks.Keys)
            {
                Console.WriteLine($"Name: {key}, Id: {tasks[key].Id}, Value: {tasks[key].Result}");
            }
        }

        public static async Task<int> SquareAsync(int n)
        {
            await Task.Delay(3000);
            return n * n;
        }
    }
}
