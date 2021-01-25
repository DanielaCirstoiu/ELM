using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa;
using TemaCasa.Entities;
using TemaCasa.Helpers;

namespace TemaCasa
{
    public class methodKrylov
    {
        private static int n;
        internal MatrixF funct = new MatrixF();
        private static Matrix A;
        private static Matrix y0;
        private static Matrix[] y;
        private static double[] q;
        private static double[] p;
        private static double[] lambda;
        private static string filePath;

        private void buildY()
        {
            y = new Matrix[n];
            for (int i = 0; i < n; i++)
                y[i] = new Matrix(n,1);
            y[0] = funct.MultiplyM(A, y0);

            for (int i = 1; i < n; i++)
            {
                y[i] = funct.MultiplyM(A, y[i - 1]);
            }
        }


        public double[] solveSystem(string filePath)
        {
            Matrix q1;
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            q1 = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    q1._matrix[i, j] = y[n - j - 2]._matrix[i,0];
                }
                q1._matrix[i,n - 1] = y0._matrix[i, 0];
            }
            writer.Write("Matrix Q = [y(n-1) y(n-2) ... y(0)] : \n");
            writer.Write(q1.Print()); ;
            writer.Write('\n');

            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = -y[n - 1]._matrix[i,0];
            }
            Cramer cramer = new Cramer(q1, b);
            q = cramer.solve();
            writer.Write("Solve ecuation! The roots q1, q2,.., qn are... ");
            for (int i = 0; i < n; i++)
            {
                writer.Write(q[i] + " ");
            }
            String polynom = "lambda ^ " + n;
            for (int i = n - 1; i > 0; i--)
            {
                polynom += (" + " + q[n - i - 1] + " * lambda ^ " + i);
            }
            polynom += " + " + q[n - 1];
            writer.Write('\n');
            writer.Write("The polynomial is " + polynom + '\n');
            writer.Close();
            return q;
        }

        // metoda pentru printarea unei matrici
        private static void printMatrix(Matrix matrix, string filePath)
        {
            FileHandler.writeFile(filePath,matrix.Print());
        }

        // matrix multiplication
        public Matrix multiplyMatr(Matrix a, Matrix b)
        {
            Matrix product = new Matrix(a.lin, b.col);
            for (int i = 0; i < a.lin; i++)
            {
                for (int j = 0; j < b.col; j++)
                {
                    for (int k = 0; k < a.col; k++)
                    {
                        product._matrix[i, j] += a._matrix[i, k] * b._matrix[k, j];
                    }
                }
            }
            return product;
        }

        public double[] Eigenvalues(double[] q,string filePath, ref StringBuilder result)
        {
            Vector p;
            int n = q.Length;
            p = new Vector(n + 1);
            for (int i = n - 1; i >= 0; i--)
            {
                p._matrix[n - i - 1] = q[i];
            }
            p._matrix[n] = 1;
            int degree = n - 1;
            Vector lambda = new Vector(n);
            int foundEv = 0;
            if (q[n - 1] == 0.0 || q[n - 1] == -0.0)
            {
                lambda._matrix[foundEv] = 0.0;
                foundEv++;
                degree = n - 2;
            }
            // cautam printre divizorii termenului liber
            for (double div = 1.0; div <= Math.Abs(q[degree]); div++)
            {
                if (Math.Abs(q[degree]) % div == 0.0)
                {
                    Horner horner = new Horner();
                    Vector temp = p.Clone();
                    while (horner.eval(div, temp) == 0.0 || horner.eval(-div, temp) == 0.0)
                    {
                        if (horner.eval(div, temp) == 0.0 || horner.eval(div, temp) == -0.0)
                        {
                            lambda._matrix[foundEv] = div;
                            foundEv++;
                            horner.calculate(div, temp);
                            temp = horner.getRes();
                        }
                        if (horner.eval(-div, temp) == 0.0 || horner.eval(-div, temp) == -0.0)
                        {
                            lambda._matrix[foundEv] = -div;
                            foundEv++;
                            horner.calculate(-div, temp);
                            temp = horner.getRes();
                        }
                    }
                }
            }
            result.Append("Eigenvalues: ");
            for (int i = 0; i < n; i++)
            {
                result.Append("Lambda" + (i + 1) + " = " + lambda._matrix[i] + " ");
            }
            return lambda._matrix;
        }

        private static void EigenVectors(double[] paux,string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            Vector lambda;
            Vector p = new Vector(paux.Length);
            for (int i = 0; i < paux.Length; i++)
                p._matrix[i] = paux[i];
            lambda = new Vector(p.lin);
            Horner horner = new Horner();
            for (int i = 0; i < p.lin; i++)
            {
                horner.eval(lambda._matrix[i], p);
                horner.calculate(lambda._matrix[i], p);
                Vector L = new Vector(p.lin);
                L = horner.getRes();

                String poly = "lambda ^ " + n;
                int k = 0;
                while (L._matrix[k] != 0.0 && k != L.lin - 1)
                {
                    poly += " + " + L._matrix[k] + " * lambda ^ " + (n - k - 1);
                    //lambda^2 -34 lambda + 33
                    k++;
                }
                writer.Write("L" + (i) + " : " + poly);
                Matrix x = y[n - 2];
                for (int j = 1; j < n - 1; j++)
                {
                    //y1 + y0*L0
                    //y1 + y0*L1
                    x = addMatrices(x, multiplyMatrixWithScalar(y[n - j - 2], L._matrix[j]), n, 1);
                }
                x = addMatrices(x, multiplyMatrixWithScalar(y0, L._matrix[L.lin - 2]), n, 1);
                x = y[n - 2];
                x = addMatrices(x, multiplyMatrixWithScalar(y0, L._matrix[L.lin - 1]), n, 1);
                writer.Write('\n');
                writer.Write("X" + (i) + ": \n");
                writer.Write(x.Print());
                
            }
            writer.Close();
        }

        private static Matrix addMatrices(Matrix a, Matrix b, int lin, int col)
        {
            Matrix c = new Matrix(lin, col);
            for (int i = 0; i < lin; i++)
                for (int j = 0; j < col; j++)
                    c._matrix[i, j] = a._matrix[i, j] + b._matrix[i, j];
            return c;
        }

        private static Matrix multiplyMatrixWithScalar(Matrix a, double x)
        {
            Matrix c = new Matrix(a.lin, a.col);
            for (int i = 0; i < a.lin; i++)
                for (int j = 0; j < a.col; j++)
                    c._matrix[i, j] = a._matrix[i, j] * x;
            return c;
        }

        public void mainKrylov(Matrix a, string filePath)
        {
            StreamWriter writer;
            writer = new StreamWriter(filePath);
            StringBuilder result = new StringBuilder();
            writer.Write("Matrix " + a.Name + '\n');
            n = a.lin;
                A = new Matrix(n, n);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        A._matrix[i,j] = a._matrix[i,j];
                y0 = new Matrix(n, 1);
                y0._matrix[0, 0] = 1;
                for (int i = 1; i < n; i++)
                    y0._matrix[i,0] = 0;

                writer.Write("Matrix A: \n");
                writer.Write(A.Print());
                writer.Write('\n');
                writer.Write("Matrix y0:\n");
                writer.Write(y0.Print());
                writer.Write('\n');
                buildY();
                for (int i = 0; i < n; i++)
                {
                    writer.Write("Matrix y" + (i + 1) + '\n');
                    writer.Write(y[i].Print());
                    writer.Write('\n');
                }
                writer.Close();
            
                //solveSystem(filePath);
                //Eigenvalues(solveSystem(filePath),filePath);
                EigenVectors(Eigenvalues(solveSystem(filePath), filePath,ref result),filePath);
                writer.Write(result);
                writer.Close();
            }
        
    }
}


