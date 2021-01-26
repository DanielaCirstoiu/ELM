using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa.Subjects.Sub3
{
    public class Sub3
    {
        public void methodCramerExecute()
        {
            string filepath = "sub1in.txt";
            string fileout = @"Sub2\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);
            double[] b;
            methodCramer mc = new methodCramer();

            //Metoda Krylov:
            foreach (var a in matrices)
            {
                mc.mainDirectInverse(a, fileout + "III_A_RED_" + a.Name + ".txt");
            }

        }
    }
}
