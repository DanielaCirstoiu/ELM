using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;
using TemaCasa.Helpers;

namespace TemaCasa
{
    public class methodDirectPower
    {
        MatrixF funct = new MatrixF();
        private static Matrix[] y1 = new Matrix[20];
        private static double[] lambda = new double[20];
        public void mainDirectPower(Matrix a, string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            StringBuilder result = new StringBuilder();
            int n = a.lin;
            a = new Matrix(n, n);
            Matrix y = new Matrix(n, 1);
            Matrix x = new Matrix(1, n);
            
            result.Append("Matrix " + a);
            for (int i = 0; i < n; i++)
            {
                y._matrix[i,1] = 1;
            }
            result.Append("Matrix Y " + y);
            y1[0] = y;
            int max = 10;
            for (int i = 0; i < max; i++)
            {
                y1[i] = funct.MultiplyM(a, y1[i - 1]);
                result.Append("Matrix Y[" + i +"]= "+y1[i]);
            }
            double lambdaRounded = 0;
            result.Append("lambda values ");
            for (int i = 0; i < n; i++)
            {
                lambda[i] = y1[max]._matrix[i,0] / y1[max - 2]._matrix[i,0];
                result.Append(lambda[i] + " ");
                lambdaRounded += lambda[i];
            }
            result.Append('\n');
            result.Append("Lambda " + lambdaRounded / n + '\n');
            lambdaRounded = Math.Round(lambdaRounded / n);
            result.Append("Lambda rounded " + lambdaRounded + '\n');

            double norma = 0;
            for (int i = 0; i < n; i++)
            {
                norma += y1[max]._matrix[i,0] * y1[max]._matrix[i,0];
            }
            norma = Math.Sqrt(norma);
            result.Append("Norma " + norma + '\n');

            for (int i = 0; i < n; i++)
            {
                x._matrix[0,i] = y1[max]._matrix[i,0] / norma;
            }
            result.Append("Matrix X : " + x);
            writer.Write(result);
            writer.Close();
        }
    }
}
