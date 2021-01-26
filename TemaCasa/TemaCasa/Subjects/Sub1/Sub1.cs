using System;
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
            string filepath = "sub1in.txt";
            string fileout = @"Sub1\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);

            methodKrylov mk = new methodKrylov();

            //Metoda Krylov:
            foreach (var a in matrices)
            {
                mk.mainKrylov(a, fileout+"I_A_RED_"+ a.Name +".txt" );
            }

        }

        public void methodDanilevskiExecute()
        {
            string filepath = "sub1in.txt";
            string fileout = @"Sub1\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);

            methodDanilevski md = new methodDanilevski();

            //Metoda Danilevski:
            foreach (var a in matrices)
            {
                md.mainDanilevski(a, fileout + "I_B_RED_" + a.Name + ".txt");
            }

        }

        public void methodDirectPowerExecute()
        {
            string filepath = "sub1in.txt";
            string fileout = @"Sub1\";
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix(filepath);

            methodDirectPower mdp = new methodDirectPower();

            //Metoda DirectPower:
            foreach (var a in matrices)
            {
                mdp.mainDirectPower(a, fileout + "I_C_RED_" + a.Name + ".txt");
            }

        }

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
