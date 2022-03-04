using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** ParallelExamples *****\n");
            Console.WriteLine("=> Main() Started");

            InvokeExample();
            ForExample();
            ForEachExample();

            Console.WriteLine("\n=> Main() Ended");
        }

        static void InvokeExample()
        {
            Console.WriteLine("\n=> InvokeExample() Begin");

            Parallel.Invoke(
                () =>
                {
                    Console.WriteLine($"Выполнение задачи №{Task.CurrentId}");
                    Thread.Sleep(1000);
                },
                Print,
                () => Square(7)
                );

            Console.WriteLine("=> InvokeExample() End");

            void Print()
            {
                Console.WriteLine($"Выполнение задачи №{Task.CurrentId}");
                Thread.Sleep(1000);
            }

            void Square(int n)
            {
                Print();
                Console.WriteLine($"Результат :{n * n}");
            }
        }

        static void ForExample()
        {
            Console.WriteLine("\n=> ForExample() Begin");

            ParallelLoopResult result = Parallel.For(1, 5, Square);

            if (!result.IsCompleted)
                Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");

            Console.WriteLine("=> ForExample() End");

            void Square(int n, ParallelLoopState pls)
            {
                if (n == 2) pls.Break();

                Console.WriteLine($"Выполнение задачи №{Task.CurrentId}");
                Thread.Sleep(1000);
                Console.WriteLine($"Квадрат числа {n} равен :{n * n}");
            }
        }

        static void ForEachExample()
        {
            Console.WriteLine("\n=> ForEachExample() Begin");

            ParallelLoopResult result = Parallel.ForEach(new List<int> { 1, 3, 5, 7 }, Square);

            if (!result.IsCompleted)
                Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");

            Console.WriteLine("=> ForEachExample() End");

            void Square(int n, ParallelLoopState pls)
            {
                if (n == 3) pls.Break();

                Console.WriteLine($"Выполнение задачи №{Task.CurrentId}");
                Thread.Sleep(1000);
                Console.WriteLine($"Квадрат числа {n} равен :{n * n}");
            }
        }
    }
}
