using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;
using TemaCasa.Helpers;

namespace TemaCasa.Subjects.Sub3
{
    public class methodCramer
    {
        MatrixF funct = new MatrixF();
        Cramer cramer = new Cramer();
        public void mainCramer(Matrix a, Matrix b, string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            StringBuilder result = new StringBuilder();


            writer.Write(result);
            writer.Close();

        }
    }
}
