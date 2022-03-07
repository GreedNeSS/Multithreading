using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Repository repository = new();
            IAsyncEnumerable<string> names = repository.GetDataAsync();

            await foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
