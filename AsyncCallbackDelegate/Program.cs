using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace AsyncCallbackDelegate
{
    class Program
    {
        public delegate int BinaryOp(int a, int b);

        private static bool isDone = false;

        static void Main(string[] args)
        {
            Console.WriteLine("***** Async Callback Delegate Example *****");
            Console.WriteLine($"Main() invoked to thread: {Thread.CurrentThread.ManagedThreadId}");

            BinaryOp binaryOp = new BinaryOp(Add);
            IAsyncResult ar = binaryOp.BeginInvoke(10, 10, 
                new AsyncCallback(AddComplite), "Main() thanks you for adding these numbers.");

            while (!isDone)
            {
                Console.WriteLine("Work ...");
                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }

        static int Add(int a, int b)
        {
            Console.WriteLine($"Add() invoked to thread: {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(5000);

            return a + b;
        }

        static void AddComplite(IAsyncResult iar)
        {
            Console.WriteLine($"AddComplite() invoked to thread: {Thread.CurrentThread.ManagedThreadId}");

            AsyncResult ar = (AsyncResult)iar;
            BinaryOp binaryOp = (BinaryOp)ar.AsyncDelegate;

            int answer = binaryOp.EndInvoke(iar);
            string mes = (string)ar.AsyncState;

            Console.WriteLine($"10 + 10 is {answer}");
            Console.WriteLine(mes);

            isDone = true;
        }
    }
}
