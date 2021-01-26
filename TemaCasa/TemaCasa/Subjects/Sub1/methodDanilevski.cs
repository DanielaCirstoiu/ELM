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
    public class methodDanilevski
    {
        private static int n;
        internal MatrixF funct = new MatrixF();
        private static Matrix a;

        public void mainDanilevski(Matrix a, string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            StringBuilder result = new StringBuilder();
            n = a.lin;
            Matrix m = new Matrix(n, n);
            Matrix pc = new Matrix(n, 1);
            int i;
            int j;            
            result.Append("Matrix : " + a);
            for (i = 0; i < m.lin-1; i++)
            {
                int k;
                for (j = 0; j < m.lin; j++)
                    for (k = 0; k < m.col; k++)
                        m._matrix[j,k] = (k == j) ? 1 : 0;
                for (j = 0; j < m.col; j++)
                    if (j == n - i)
                        m._matrix[n - i-2,j] = 1.0 / a._matrix[n - i-1,n - i-1];
                    else
                        m._matrix[n - i-2,j] = -a._matrix[n - i-2+j,j] / a._matrix[n - i-1,n - i-2];
                Matrix m_1 = funct.Inverse(m);
                a = funct.MultiplyM(m_1, funct.MultiplyM(a, m));
            }
            pc._matrix[0,0] = 1;
            if (a.lin % 2 == 1)
                for (i = 0; i < pc.lin; i++)
                    pc._matrix[i,0] = pc._matrix[i,0];
            for (i = 0; i < pc.lin; i++)
                pc._matrix[i,0] = -a._matrix[0,i];
            for (i = 0; i < pc.lin; i++)
                result.Append("coef " + i + " " + pc._matrix[i,0] + '\n');
            writer.Write(result);
            writer.Close();
        }
    }
}
