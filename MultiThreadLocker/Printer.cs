using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThreadLocker
{
    public class Printer
    {
        private object threadLocker = new object();
        public void PrintNumbers()
        {
            lock (threadLocker)
            {
                Console.WriteLine($"=> {Thread.CurrentThread.Name} is executing PrintNumbers()");
                Console.Write("Your numbers: ");

                for (int i = 0; i < 10; i++)
                {
                    Random random = new Random();
                    Thread.Sleep(100 * random.Next(20));

                    Console.Write($"{i}, ");
                }

                Console.WriteLine();
            }
        }
    }
}
