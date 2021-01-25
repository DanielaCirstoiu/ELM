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
        public void methodKrylov()
        {
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix("sub1sub1in.txt");
            

            //Metoda Krylov:
            foreach (var a in matrices)
            {
                FileHandler.writeFile("I_A_RED.txt", methodKrylov().Print());
            }

        }

        public void methodDanilevski()
        {
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix("in.txt");


            //Metoda Danilevski:
            foreach (var b in matrices)
            {
                FileHandler.writeFile("I_B_RED.txt", b.methodDanilevski().Print());
            }

        }

        public void methodDirect()
        {
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix("in.txt");


            //Metoda Direct:
            foreach (var b in matrices)
            {
                FileHandler.writeFile("I_C_RED.txt", b.methodDirect().Print());
            }

        }

        public void methodLeverriere()
        {
            //citeste matricea
            List<Matrix> matrices = FileHandler.readFileforMatrix("in.txt");


            //Metoda Leverriere:
            foreach (var b in matrices)
            {
                FileHandler.writeFile("I_F_RED.txt", b.methodLeverriere().Print());
            }

        }


        //det polinoamele caracteristice
        //det valorilor proprii
        //det vectorilor proprii
        //scrierea rezultatelor intr-un fisier text
    }
}
