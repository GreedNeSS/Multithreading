using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SimpleMultiThreadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Simple Multi Thread App *****");
            Console.Write("Do you want [1] or [2] threads? ");
            string threadCount = Console.ReadLine();

            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            Console.WriteLine($"=> {primaryThread.Name} is executing Main()");

            Printer printer = new Printer();

            switch (threadCount)
            {
                case "2":
                    Thread backgroundThread = new Thread(new ThreadStart(printer.PrintNumbers));
                    backgroundThread.Name = "Secondary";
                    backgroundThread.Start();
                    break;
                case "1":
                    printer.PrintNumbers();
                    break;
                default:
                    Console.WriteLine("I don't know what...you get 1 thread.");
                    goto case "1";
            }

            MessageBox.Show("I'm bussy!", "Work on main thread...");
            Console.ReadLine();
        }
    }
}
