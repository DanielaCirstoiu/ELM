﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;
using TemaCasa.Helpers;

namespace TemaCasa.Subjects.Sub2
{
    class methodGaussJordan
    {
        MatrixF funct = new MatrixF();
        internal void mainGaussJordan(Matrix a, string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            StringBuilder result = new StringBuilder();
            result.Append(funct.Inverse(a).Print());
            writer.Write(result);
            writer.Close();
        }
    }
}
