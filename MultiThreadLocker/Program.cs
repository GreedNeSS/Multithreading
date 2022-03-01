using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThreadLocker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Multiple Thread Locker *****\n");

            Printer p = new Printer();

            Thread[] threads = new Thread[10];

            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"Worker thread #{i}"
                };
            }

            foreach (Thread thread in threads)
            {
                thread.Start();
            }
        }
    }
}
