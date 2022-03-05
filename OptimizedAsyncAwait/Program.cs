using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OptimizedAsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string[] names = { "Ruslan", "Marcus", "Henry" };

            Console.WriteLine("***** Sync code *****\n");

            foreach (var name in names)
            {
                PrintNameSync(name);
            }

            Console.WriteLine("\n***** Async code *****\n");

            foreach (var name in names)
            {
                await PrintNameAsync(name);
            }

            Console.WriteLine("\n***** Optimized async code *****\n");

            Task[] tasks = new Task[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                tasks[i] = PrintNameAsync(names[i]);
            }

            foreach (Task task in tasks)
            {
                await task;
            }
        }

        static void PrintNameSync(string name)
        {
            Thread.Sleep(3000);
            Console.WriteLine($"Hello {name} from PrintNameSync()");
        }

        static async Task PrintNameAsync(string name)
        {
            await Task.Delay(3000);
            Console.WriteLine($"Hello {name} from PrintNameAsync()");
        }
    }
}
