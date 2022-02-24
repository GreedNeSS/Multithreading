using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadStats
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Prymary Thread Stat *****\n");

            Thread thread = Thread.CurrentThread;
            thread.Name = "ThePrimaryThread";

            Console.WriteLine("Name of current AppDomain: " +
                Thread.GetDomain().FriendlyName);
            Console.WriteLine("ID of current Context: " + 
                Thread.CurrentContext.ContextID);
            Console.WriteLine("Thread Name: " + 
                thread.Name);
            Console.WriteLine("Has thread started?: " +
                thread.IsAlive);
            Console.WriteLine("Priority Level: " +
                thread.Priority);
            Console.WriteLine("Thread state: " +
                thread.ThreadState);

            Console.ReadLine();
        }
    }
}
