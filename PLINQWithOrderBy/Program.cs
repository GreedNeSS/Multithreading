using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace PLINQWithOrderBy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** PLINQ With Order By *****");

            int[] nums = { 5, 6, 8, -10, -8, 1, 3, -4, -1, 0, 10, 50 };

            PLINQForAllExample(nums);
            PLINQOrderByExample(nums);
            PLINQAsOrderedExample(nums);
            PLINQAsUnorderedExample(nums);
        }

        static void PLINQForAllExample(int[] nums)
        {
            Console.WriteLine("=> PLINQForAllExample():");

            var results = nums.AsParallel().Select(n => n * n);

            try
            {
                results.ForAll(n => Console.WriteLine(n));
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void PLINQOrderByExample(int[] nums)
        {
            Console.WriteLine("\n=> PLINQOrderByExample():");

            var results = from n in nums.AsParallel()
                          where n > 0
                          orderby n
                          select Square(n);

            foreach (var res in results)
            {
                Console.WriteLine($"Result: {res}");
            }

            int Square(int n)
            {
                return n * n;
            }
        }

        static void PLINQAsOrderedExample(int[] nums)
        {
            Console.WriteLine("\n=> PLINQAsOrderedExample():");

            var results = from n in nums.AsParallel().AsOrdered()
                          where n > 0
                          select n * n;

            foreach (var res in results)
            {
                Console.WriteLine($"Result: {res}");
            }
        }

        static void PLINQAsUnorderedExample(int[] nums)
        {
            Console.WriteLine("\n=> PLINQAsUnorderedExample():");

            var res1 = from n in nums.AsParallel().AsOrdered()
                          where n > 0
                          select n * n;

            var res2 = from n in res1.AsUnordered()
                       where n > 3
                       select n;

            foreach (var res in res2)
            {
                Console.WriteLine($"Result: {res}");
            }
        }
    }
}
