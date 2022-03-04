using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreApp
{
    class Reader
    {
        static Semaphore semaphore = new Semaphore(3, 3);

        Thread thread;
        int count = 3;

        public Reader(int i)
        {
            thread = new Thread(Read);
            thread.Name = $"Читатель {i}";
            thread.Start();
        }

        public void Read()
        {
            while (count > 0)
            {
                semaphore.WaitOne();

                Console.WriteLine($"{Thread.CurrentThread.Name} заходит в библиотеку!");
                Console.WriteLine($"{Thread.CurrentThread.Name} читает!");

                Thread.Sleep(500);

                Console.WriteLine($"{Thread.CurrentThread.Name} выходит из библиотеки!");

                semaphore.Release();
                
                count--;

                Thread.Sleep(1000);
            }
        }
    }
}
