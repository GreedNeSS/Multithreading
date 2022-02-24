using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncDelegate
{
    class Program
    {
        public delegate int BinaryOp(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("***** Async Delegate Review *****");
            Console.WriteLine($"Main() invoked to thread: {Thread.CurrentThread.ManagedThreadId}");

            BinaryOp binaryOp = new BinaryOp(Add);
            IAsyncResult ar = binaryOp.BeginInvoke(10, 10, null, null);

            //while (!ar.IsCompleted)
            //{
            //    Console.WriteLine("Doing more work in Main().");
            //    Thread.Sleep(1000);
            //}

            while (!ar.AsyncWaitHandle.WaitOne(1000, true))
            {
                Console.WriteLine("Doing more work in Main().");
            }

            int answer = binaryOp.EndInvoke(ar);
            
            Console.WriteLine($"10 + 10 is {answer}");
            Console.ReadLine();
        }

        static int Add(int a, int b)
        {
            Console.WriteLine($"Add() invoked to thread: {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(5000);

            return a + b;
        }
    }
}
