﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa
{
    //Subiectul I
    class Sub1
    {     
        public void methodKrylovExecute()
        {
            string filepath = @"C:\Users\CE01812465\OneDrive - DRAEXLMAIER\Daniela\Git\MEL\TemaCasa\TemaCasa\bin\Debug\sub1in.txt";
            string fileout = @"C:\Users\CE01812465\OneDrive - DRAEXLMAIER\Daniela\Git\MEL\TemaCasa\TemaCasa\bin\Debug\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);

            methodKrylov mk = new methodKrylov();

            //Metoda Krylov:
            foreach (var a in matrices)
            {
                mk.mainKrylov(a, fileout+"I_A_RED."+ a.Name +".txt" );
            }

        }

        //public void methodDanilevski()
        //{
        //    //citeste matricea
        //    List<Matrix> matrices = FileHandler.readFileforMatrix("in.txt");


        //    //Metoda Danilevski:
        //    foreach (var b in matrices)
        //    {
        //        FileHandler.writeFile("I_B_RED.txt", b.methodDanilevski().Print());
        //    }

        //}

        //public void methodDirect()
        //{
        //    //citeste matricea
        //    List<Matrix> matrices = FileHandler.readFileforMatrix("in.txt");


        //    //Metoda Direct:
        //    foreach (var b in matrices)
        //    {
        //        FileHandler.writeFile("I_C_RED.txt", b.methodDirect().Print());
        //    }

        //}

        //public void methodLeverriere()
        //{
        //    //citeste matricea
        //    List<Matrix> matrices = FileHandler.readFileforMatrix("in.txt");


        //    //Metoda Leverriere:
        //    foreach (var b in matrices)
        //    {
        //        FileHandler.writeFile("I_F_RED.txt", b.methodLeverriere().Print());
        //    }

        //}


        //det polinoamele caracteristice
        //det valorilor proprii
        //det vectorilor proprii
        //scrierea rezultatelor intr-un fisier text
    }
}
