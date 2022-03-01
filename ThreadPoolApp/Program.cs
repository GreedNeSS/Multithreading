using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadPoolApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Thread Pool App *****\n");

            Printer p = new Printer();

            WaitCallback callback = new WaitCallback(PrintTheNumbers);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(callback, p);
            }

            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer printer = (Printer)state;
            printer.PrintNumbers();
        }
    }
}
