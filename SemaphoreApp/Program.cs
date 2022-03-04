using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 6; i++)
            {
                Reader reader = new Reader(i);
            }

            Thread.Sleep(1000);
            Reader.SemaphoreRelease(5);
        }
    }
}
