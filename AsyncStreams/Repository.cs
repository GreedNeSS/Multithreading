using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Repository
    {
        string[] data = { "Tom", "Sem", "Kate", "Ruslan", "Marcus" };
        public async IAsyncEnumerable<string> GetDataAsync()
        {
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine($"Получаем {i + 1} элемент");
                await Task.Delay(500);
                yield return data[i];
            }
        }
    }
}
