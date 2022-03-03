using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLINQDataProcessingWithCancellation
{
    class Program
    {
        private static CancellationTokenSource cancelToken = new CancellationTokenSource();
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Start any key to start processing");
                Console.ReadKey();

                Console.WriteLine("=> Processing...");
                Task.Factory.StartNew(() => ProcessIntData());
                Console.WriteLine("Enter Q to quit: ");
                
                string answer = Console.ReadLine();

                if (answer.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    cancelToken.Cancel();
                    break;
                }

            } while (true);
        }

        static void ProcessIntData()
        {
            int[] source = Enumerable.Range(1, 10_000_000).ToArray();

            int[] modThreeIsZero = null;

            try
            {
                modThreeIsZero = (from num in source.AsParallel().WithCancellation(cancelToken.Token)
                                  where (num % 3) == 0
                                  orderby num descending
                                  select num).ToArray();

                Console.WriteLine();
                Console.WriteLine($"Found {modThreeIsZero.Count()} numbers that match query!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
