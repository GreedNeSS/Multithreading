using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexApp
{
    class Program
    {
        static Mutex mutex = new Mutex();
        static int x = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("***** Mutex App *****");

            for (int i = 0; i < 6; i++)
            {
                Thread thread = new Thread(PrintNum);
                thread.Name = $"Thread #{i}";
                thread.Start();
            }
        }

        static void PrintNum()
        {
            mutex.WaitOne();

            x = 1;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Thread Name: {Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }

            mutex.ReleaseMutex();
        }
    }
}
