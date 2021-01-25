using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaCasa
{
    class Program
    {
        static void Main(string[] args)
        {
            Sub1 sub1 = new Sub1();
            sub1.methodKrylov();
            sub1.methodDanilevski();
            sub1.methodDirect();
            sub1.methodLeverriere();
        }
    }
}
