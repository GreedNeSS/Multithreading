using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParameterizedThreadStartExample
{
    class AddParams
    {
        public int A { get; set; }
        public int B { get; set; }

        public AddParams(int a, int b)
        {
            A = a;
            B = b;
        }
    }
}
