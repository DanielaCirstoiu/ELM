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
    class methodKrylov
    {
        private static int n;

        private static Matrix A;
        private static Matrix y0;
        private static Matrix[] y;
        private static double[] q;
        private static double[] p;
        private static double[] lambda;
        private static string filePath;

        private static void buildY()
        {
            y = Matrix(n,n, 1);
            y[0] = MultiplyM(A, y0);
            for (int i = 1; i < n; i++)
            {
                y[i] = MultiplyM(A, y[i - 1]);
            }
        }


        public void solveSystem()
        {
            Matrix q1;
            StreamWriter writer;
            q1 = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    q1._matrix[i, j] = y[n - j - 2][i][0];
                }
                q1._matrix[i,n - 1] = y0._matrix[i, 0];
            }
            writer.Write("Matrix Q = [y(n-1) y(n-2) ... y(0)] : \n");
            printMatrix(q1);
            writer.Write('\n');

            double[] b = new double[n];
            for (int i = 0; i < n; i++)
            {
                b[i] = -y[n - 1][i][0];
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
            //
        }

        // metoda pentru printarea unei matrici
        private static void printMatrix(Matrix matrix)
        {
            StreamWriter writer;
            int j = -1;
            for (j = matrix.col in matrix)
            {
                j++;
                for (int i = 0; i < line.Length; i++)
                {
                    writer.Write(matrix._matrix[j, i] + " ");
                }
                writer.Write('\n');
            }
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

        public double Eigenvalues(Vector q)
        {
            StreamWriter writer;
            Vector p;
            int n = q.lin;
            p = new Vector(n + 1);
            for (int i = n - 1; i >= 0; i--)
            {
                p._matrix[n - i - 1] = p._matrix[i];
            }
            p._matrix[n] = 1;
            int degree = n - 1;
            Vector lambda = new Vector(n);
            int foundEv = 0;
            if (q._matrix[n - 1] == 0.0 || q._matrix[n - 1] == -0.0)
            {
                lambda._matrix[foundEv] = 0.0;
                foundEv++;
                degree = n - 2;
            }
            // cautam printre divizorii termenului liber
            for (double div = 1.0; div <= Math.Abs(q._matrix[degree]); div++)
            {
                if (Math.Abs(q._matrix[degree]) % div == 0.0)
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
            writer.Write("Eigenvalues: ");
            for (int i = 0; i < n; i++)
            {
                writer.Write("Lambda" + (i + 1) + " = " + lambda._matrix[i] + " ");
            }
            return lambda;
        }

        private static void EigenVectors(Vector p)
        {
            StreamWriter writer;
            Vector lambda;
            lambda = new Vector(p.lin);
            Horner horner = new Horner();
            for (int i = 0; i < p.lin; i++)
            {
                // writer.Write('\n');
                horner.eval(lambda._matrix[i], p);
                horner.calculate(lambda._matrix[i], p);
                Vector L = horner.getRes();

                String poly = "lambda ^ " + n;
                int k = 1;
                while (L._matrix[k] != 0.0 && k != L.lin - 1)
                {
                    poly += " + " + L._matrix[k] + " * lambda ^ " + (n - k - 1);
                    k++;
                }
                writer.Write("L" + (i + 1) + " : " + poly);
                Matrix x = y[n - 2];
                for (int j = 1; j < n - 1; j++)
                {
                    x = addMatrices(x, multiplyMatrixWithScalar(y[n - j - 2], L[j], n, 1), n, 1);
                }
                x = addMatrices(x, multiplyMatrixWithScalar(y0, L[L.length - 2], n, 1), n, 1);
                writer.Write('\n');
                writer.Write("X" + (i + 1) + ": \n");
                Print(x, writer);

            }

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

        public static void main()
        {
            FileHandler.readFileforMatrix("sub1in.txt");
            string text = null;
            StreamWriter writer;
            for (int line = 1; line <= 6; line++)
            {
                switch (line)
                {
                    case 1:
                        writer = new StreamWriter("results/I/Krylov/I_A_SAC.A2.txt");
                        break;
                    case 2:
                        writer = new StreamWriter("results/I/Krylov/I_A_RED.B2.txt");
                        break;
                    case 3:
                        writer = new StreamWriter("results/I/Krylov/I_A_RED.A3.txt");
                        break;
                    case 4:
                        writer = new StreamWriter("results/I/Krylov/I_A_RED.B3.txt");
                        break;
                    case 5:
                        writer = new StreamWriter("results/I/Krylov/I_A_RED.A4.txt");
                        break;
                    default:
                        writer = new StreamWriter("results/I/Krylov/I_A_RED.B4.txt");
                        break;
                }
                writer.Write('\n');
                writer.Write("Matrix " + line + '\n');
                //n = scan.nextInt();
                A = new Matrix(n, n);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        //A[i, j] = scan.nextDouble();
                y0 = new Matrix(n, 1);
                for (int i = 0; i < n; i++)
                    y0._matrix[i,0] = 1.0;

                writer.Write("Matrix A: \n");
                printMatrix(A);
                writer.Write('\n');
                writer.Write("Matrix y0:\n");
                printMatrix(y0);
                writer.Write('\n');
                buildY();
                for (int i = 0; i < n; i++)
                {
                    writer.Write("Matrix y" + (i + 1) + '\n');
                    printMatrix(y[i]);
                    writer.Write('\n');
                }
                solveSystem();
                Eigenvalues();
                EigenVectors();

                writer.Close();
            }
        }
    }
}


