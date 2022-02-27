using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ParameterizedThreadStartExample
{
    class Program
    {
        private static AutoResetEvent waitHandler = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("***** Using Parameterized Thread Start *****");
            Console.WriteLine($"ID of thread in Main(): {Thread.CurrentThread.ManagedThreadId}");

            AddParams addParams = new AddParams(27, 10);
            Thread newThread = new Thread(new ParameterizedThreadStart(Add));

            newThread.Start(addParams);

            waitHandler.WaitOne();

            Console.ReadLine();
        }

        static void Add(object addParams)
        {
            if (addParams is AddParams @params)
            {
                Console.WriteLine($"ID of thread in Add(): {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"{@params.A} + {@params.B} = {@params.A + @params.B}");
            }

            waitHandler.Set();
        }
    }
}
