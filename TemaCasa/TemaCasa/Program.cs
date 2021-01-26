using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Subjects.Sub2;

namespace TemaCasa
{
    class Program
    {
        static void Main(string[] args)
        {
            Sub1 sub1 = new Sub1();
            //sub1.methodKrylovExecute();
            sub1.methodDanilevskiExecute();
            //sub1.methodDirectPowerExecute();
            //sub1.methodLeverriere();

            Sub2 sub2 = new Sub2();
            //sub2.methodDirectInverseExecute();
        }
    }
}
