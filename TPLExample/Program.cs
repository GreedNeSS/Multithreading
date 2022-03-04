using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingClassTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** TPL ******");
            Console.WriteLine("=> Main() started");

            RunInnerOuterTasks();
            ReturnResultFromTask();
            ContinueWithExample();

            Console.WriteLine("=> Main() ended");

        }

        static void RunInnerOuterTasks()
        {
            Task outerThread = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("=> Outer task started");

                Task innerTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("=> Inner task started");
                    Thread.Sleep(2000);
                    Console.WriteLine("=> Inner Task ended");
                }, TaskCreationOptions.AttachedToParent);

                Console.WriteLine("=> Outer Task ended");
            });

            //outerThread.Wait();

            Task.WaitAll(outerThread);
        }

        static void ReturnResultFromTask()
        {
            Console.WriteLine("=> start ReturnResultFromTask(): ");

            int i = 5, j = 6;

            Task<int> sumTask = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                return i + j;
             });
            sumTask.Start();

            int result = sumTask.Result;
            Console.WriteLine($"i + j = {result}");
        }

        static void ContinueWithExample()
        {
            Console.WriteLine("=> start ContinueWithExample(): ");

            int i = 5, j = 6;

            Task<int> sumTask = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                return i + j;
            });

            Task printResultTask = sumTask.ContinueWith(task => PrintSum(task.Result));
            sumTask.Start();
            printResultTask.Wait();

            void PrintSum(int sum) => Console.WriteLine($"i + j = {sum}");
        }
    }
}
