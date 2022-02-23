using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SyncDelegateReview
{
    class Program
    {
        public delegate int BinaryOp(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("***** Sync Delegate Review *****");
            Console.WriteLine($"Main() invoked to thread: {Thread.CurrentThread.ManagedThreadId}");

            BinaryOp binaryOp = new BinaryOp(Add);
            int answer = binaryOp.Invoke(10, 10);

            Console.WriteLine("Doing more work in Main().");
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
