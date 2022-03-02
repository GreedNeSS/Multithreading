using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TimerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TimerCallback callback = new TimerCallback(PrintTime);
            var _ = new Timer(callback, "Hello Ruslan", 0, 1000);

            Console.ReadLine();
        }

        static void PrintTime(object state)
        {
            Console.WriteLine($"Time: {DateTime.Now.ToLongTimeString()} Message: {state.ToString()}");
        }
    }
}
