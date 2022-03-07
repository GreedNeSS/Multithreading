using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncExceptions
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("***** Async Exceptions *****");

            await TaskExeptionAsync();
            await TaskErrorInfoAsync();
            await MultyExceptionsFromTasksAsync();
        }

        static async Task TaskExeptionAsync()
        {
            Console.WriteLine("\n=> Sumple Example: ");

            try
            {
                await PrintAsync("Hello Greed!");
                await PrintAsync("Hi");

                PrintWithVoidAsync("Hi");

                await Task.Delay(1000);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }

        static async Task TaskErrorInfoAsync()
        {
            Console.WriteLine("\n=> TaskErrorInfoAsync(): ");

            Task task = PrintAsync("Hi");

            try
            {
                await task;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(task.Exception?.InnerException?.Message);
                Console.WriteLine($"IsFaulted: {task.IsFaulted}");
                Console.WriteLine($"Task status: {task.Status}");
                Console.ResetColor();
            }
        }

        public static async Task MultyExceptionsFromTasksAsync()
        {
            Console.WriteLine("\n=> MultyExceptionsFromTasksAsync(): ");

            Task task1 = PrintAsync("HI");
            Task task2 = PrintAsync("BB");
            var allTasks = Task.WhenAll(task1, task2);

            try
            {
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"IsFauled: {allTasks.IsFaulted}");

                if (!(allTasks.Exception is null))
                {
                    foreach (var e in allTasks.Exception.InnerExceptions)
                    {
                        Console.WriteLine($"InnerException: {e.Message}");
                    }
                }

                Console.ResetColor();
            }
        }

        static async Task PrintAsync(string message)
        {
            if (message.Length < 3)
                throw new Exception($"Invalid string length: {message.Length}");

            await Task.Delay(100);
            Console.WriteLine(message);
        }

        static async void PrintWithVoidAsync(string message)
        {
            try
            {
                if (message.Length < 3)
                    throw new Exception($"Invalid string length: {message.Length}");

                await Task.Delay(100);
                Console.WriteLine(message); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
