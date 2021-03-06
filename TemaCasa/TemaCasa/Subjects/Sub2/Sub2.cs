﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa.Subjects.Sub2
{
    public class Sub2
    {
        public void methodDirectInverseExecute()
        {
            string filepath = "sub2in.txt";
            string fileout = @"Sub2\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);

            methodDirectInverse mdi = new methodDirectInverse();

            //Metoda Krylov:
            foreach (var a in matrices)
            {
                mdi.mainDirectInverse(a, fileout + "II_A_RED_" + a.Name + ".txt");
            }

        }

        public void methodGaussJordanExecute()
        {
            string filepath = "sub2in.txt";
            string fileout = @"Sub2\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);

            methodGaussJordan mgj = new methodGaussJordan();

            //Metoda Krylov:
            foreach (var a in matrices)
            {
                mgj.mainGaussJordan(a, fileout + "II_B_RED_" + a.Name + ".txt");
            }

        }
    }
}
