using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationExample
{
    class Program
    {
        static CancellationTokenSource cancellationToken = new CancellationTokenSource();

        static void Main(string[] args)
        {
            Console.WriteLine("***** Cancellation Example *****");

            Task softTask =  SoftCancellationTaskExample();
            Task exTask = ExceptionCancellationTaskExample();
            softTask.Start();
            exTask.Start();

            try
            {
                Thread.Sleep(1000);

                cancellationToken.Cancel();

                Thread.Sleep(1000);
                exTask.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            finally
            {
                Console.WriteLine($"softTask.Status: {softTask.Status}");
                Console.WriteLine($"exTask.Status: {exTask.Status}");
                cancellationToken.Dispose();
            }
        }

        static Task SoftCancellationTaskExample()
        {
            return new Task(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (cancellationToken.Token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция прервана!");
                        return;
                    }

                    Console.WriteLine($"Work in thread #{Task.CurrentId}");

                    Thread.Sleep(300);
                }
            }, cancellationToken.Token);
        }

        static Task ExceptionCancellationTaskExample()
        {
            CancellationToken token = cancellationToken.Token;
            return new Task(() =>
            {
                token.Register(() => token.ThrowIfCancellationRequested());

                for (int i = 0; i < 10; i++)
                {

                    Console.WriteLine($"Work in thread #{Task.CurrentId}");

                    Thread.Sleep(300);
                }
            }, token);
        }
    }
}
