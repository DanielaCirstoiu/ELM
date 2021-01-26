using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;
using TemaCasa.Helpers;

namespace TemaCasa.Subjects.Sub2
{
    public class methodDirectInverse
    {
        MatrixF funct = new MatrixF();
        internal void mainDirectInverse(Matrix a, string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            StringBuilder result = new StringBuilder();
            result.Append(funct.DirectInverse(a).Print());
            writer.Write(result);
            writer.Close();
        }
    }
}
