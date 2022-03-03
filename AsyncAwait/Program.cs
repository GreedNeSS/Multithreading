using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<int> i = default;
            string message = await DoWorkAsync();
            Console.WriteLine(message);
            await MethodReturningVoidAsync();
            Console.WriteLine("Void medthod complete");
            await MethodReturningVoidAsync();
            await MethodWithProblemAsync(5, -7);
            await MethodWithProblemFixedAsync(5, -7);
            Console.WriteLine("Completed");
            Console.ReadLine();
        }

        static string DoWork()
        {
            Thread.Sleep(5000);
            return "Done with work!";
        }

        static async Task<string> DoWorkAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(100);
                return "Done with work!";
            });
            
        }
        static async Task MethodReturningVoidAsync()
        {
            await Task.Run(async() =>
            {
                await Task.Delay(1000);
                Console.WriteLine("Run MethodReturningVoidAsync");
            });
            Console.WriteLine("MethodReturningVoidAsync completed");
        }
        static async ValueTask<int> ReturnAnInt()
        {
            await Task.Delay(1000);
            return 5;
        }

        static async Task MethodWithProblemAsync(int firstParam, int secondParam)
        {
            Console.WriteLine("Enter");
            await Task.Run(async() =>
            {
                await Task.Delay(500);
                Console.WriteLine("First complete");
                Console.WriteLine("Something bad happend");
            });
        }

        static async Task MethodWithProblemFixedAsync(int firstParam, int secondParam)
        {
            Console.WriteLine("Enter");

            if (secondParam < 0)
            {
                Console.WriteLine("Bad data");
                return;
            }

            await actualImpletentation();

            async Task actualImpletentation()
            {
                await Task.Run(() =>
                {
                    Task.Delay(500);
                    Console.WriteLine("First complete");
                    Console.WriteLine("Something bad happend");
                });
            }
        }
    }
}
