using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa.Helpers
{
    public class MatrixF
    {
        public static int nmax = 20;

        public Matrix power(Matrix a, int n)
        {
            Matrix res = a.Clone();
            if (n >= 2)
            {
                for (int i = 2; i <= n; i++)
                    res = MultiplyM(res, a);
            }
            return res;
        }

        //private void helper(List<int[]> combinations, int[] data, int start, int end, int index)
        //{
        //    if (index == data.Length)
        //    {
        //        int[] combination = data.Clone();
        //        combinations.Add(combination);
        //    }
        //    else if (start <= end)
        //    {
        //        data[index] = start;
        //        helper(combinations, data, start + 1, end, index + 1);
        //        helper(combinations, data, start + 1, end, index);
        //    }
        //}


        //public List<Double> CharactPolynomial(Matrix matrix)
        //{
        //    ArrayList pol = new ArrayList();
        //    if (matrix.lin != matrix.col)
        //    {
        //        Console.WriteLine("Matrix is not square");
        //        //return pol;
        //    }
        //    int n = matrix.lin;
        //    double p = TraceM(matrix);
        //    pol.Add(1.0);
        //    pol.Add(p);
        //    if (n == 1)
        //    {
        //        Console.WriteLine("The dimension of the matrix is 1");
        //    }
        //    else
        //    {
        //        int k = 2;
        //        while (k <= n)
        //        {
        //            double sum = 0.0;
        //            List<Matrix> combinations = new List<Matrix>();
        //            helper(combinations, new int[k], 1, n, 0);
        //            foreach (var combination in combinations)
        //                sum += Det(buildMatrix(matrix, combination));
        //            pol.Add(sum);
        //            k++;
        //        }
        //    }
        //    return pol;
        //}


        public Matrix det_pol_car(Matrix a)
        {
            Matrix ret, b, y, c;
            int i, j;
            //double aux;
            y = new Matrix(a.lin, 1);
            b = new Matrix(a.lin, a.col);
            c = new Matrix(a.lin, 1);
            y._matrix[0, 0] = 1;
            for (i = 1; i < y.lin; i++)
                y._matrix[i, 0] = 0;
            for (i = 0; i < b.lin; i++)
                b._matrix[i, b.col - 1] = y._matrix[i, 0];
            for (i = 0; i < a.lin - 1; i++)
            {
                y = MultiplyM(a, y);
                for (j = 0; j < b.lin; j++)
                    b._matrix[j, b.col - i - 1] = y._matrix[j, 0];
            }
            y = MultiplyM(a, y);
            for (i = 0; i < b.lin; i++)
                c._matrix[i, 0] = -y._matrix[i, 0];
            ret = SolveSystem(b, c);
            ret._matrix[0, 1] = 1;
            return ret;
        }


        #region Operations

        /// Addition of 2 Matrices // return c = a + b
        public Matrix AddM(Matrix a, Matrix b)
        {
            if (b.lin != a.lin || b.col != a.col) throw new Exception("Illegal matrix dimensions.");
            Matrix c = new Matrix(a.lin, a.col);
            for (int i = 0; i < c.lin; i++)
                for (int j = 0; j < c.col; j++)
                    c._matrix[i, j] = a._matrix[i, j] + b._matrix[i, j];
            return c;
        }

        /// Substraction of a Matrix // return c = a - b
        public Matrix SubstractM(Matrix a, Matrix b)
        {
            if (b.lin != a.lin || b.col != a.col) throw new Exception("Illegal matrix dimensions.");
            Matrix c = new Matrix(a.lin, a.col);
            for (int i = 0; i < c.lin; i++)
                for (int j = 0; j < c.col; j++)
                    c._matrix[i, j] = a._matrix[i, j] - b._matrix[i, j];
            return c;
        }

        /// Scalar multiplication of a matrix
        public static Matrix MultiplyScalar(Matrix a, double scalar)
        {
            Matrix c = new Matrix(a.lin, a.col);
            for (int i = 0; i < a.lin; i++)
                for (int j = 0; j < a.col; j++)
                    c.val(i, j, a.val(i, j) * scalar);
            return c;
        }

        /// Matrix multiplication
        public Matrix MultiplyM(Matrix a, Matrix b)
        {
            int i, j, k;
            Matrix ret;
            ret = new Matrix(a.lin, b.col);
            for (i = 0; i < ret.lin; i++)
                for (j = 0; j < ret.col; j++)
                {
                    ret._matrix[i, j] = 0;
                    for (k = 0; k < a.col; k++)
                        ret._matrix[i, j] += a._matrix[i, k] * b._matrix[k, j];
                }
            return ret;
        }

        /// Swap rows l1 and l2
        public Matrix SwapLin(Matrix a, int l1, int l2)
        {
            Matrix b = a.Clone();
            for (int i = 1; i <= a.col; i++)
            {
                b._matrix[l1,i] = a._matrix[l2,i];
                b._matrix[l2,i] = a._matrix[l1,i];
            }
            return b;
        }

        ///Swap columns
        Matrix SwapCol(Matrix a, Matrix b, int k)
        {
            Matrix ret;
            int i;
            ret = a;
            for (i = 1; i <= a.lin; i++)
                ret._matrix[i, k] = b._matrix[i, 1];
            return ret;
        }

        ///Unit Matrix
        public Matrix UnitMatrix(int dim)
        {
            int dimension = dim;
            if (dim > dimension)
                dimension = dim;
            Matrix c = new Matrix(dimension, dimension);
            for (int i = 0; i < dimension; i++)
                c.val(i, i, 1.0);
            return c;
        }


        /// Solves the system of liniar equations
        public Matrix SolveSystem(Matrix a, Matrix b)
        {
            Matrix ret, aextins;
            aextins = new Matrix(a.lin, a.col + 1);

            double max, aux, pivot;
            int i, j, k, poz;
            for (i = 0; i < aextins.lin; i++)
                for (j = 0; j < a.col; j++)
                    aextins._matrix[i, j] = a._matrix[i, j];
            for (i = 0; i < aextins.lin; i++)
                aextins._matrix[i, aextins.col - 1] = b._matrix[i, 0];
            for (i = 0; i < aextins.lin; i++)
            {
                max = Math.Abs(aextins._matrix[i, i]);
                poz = i;
                for (j = i; j < aextins.lin; j++)
                    if (Math.Abs(aextins._matrix[j, i]) > max)
                    {
                        max = Math.Abs(aextins._matrix[j, i]);
                        poz = j;
                    }
                if (max == 0)
                {
                    Console.WriteLine("matricea sistemului este singulara");
                    System.Environment.Exit(1);
                }
                if (poz - 1 != i)
                    for (j = 0; j < aextins.col; j++)
                    {
                        aux = aextins._matrix[i, j];
                        aextins._matrix[i, j] = aextins._matrix[poz, j];
                        aextins._matrix[poz, j] = aux;
                    }
                for (j = i; j < aextins.lin; j++)
                {
                    pivot = a._matrix[j, i] / aextins._matrix[i, i];
                    for (k = 0; k < aextins.lin; k++)
                        if (i != k)
                            //aextins._matrix[k,j] = (a._matrix[i,i] * a._matrix[k,j] - a._matrix[k,i] * a._matrix[i,j]) / a._matrix[i,i];
                            aextins._matrix[j, k] = aextins._matrix[j, k] - ((aextins._matrix[k, j] * pivot));
                }
            }
            ret = new Matrix(a.lin, 1);
            for (i = ret.lin - 1; i >= 0; i--)
            {
                aux = 0;
                for (j = ret.lin - 1; j > i - 1; j--)
                    aux += aextins._matrix[i, j] * ret._matrix[j, 0];
                ret._matrix[i, 0] = 1.0 / aextins._matrix[i, i] * (aextins._matrix[i, aextins.col - 1] - aux);
            }
            return ret;
        }


        /// Transpose Matrix
        public Matrix Transpose(Matrix a)
        {
            Matrix ret = new Matrix(a.col, a.lin);
            for (int i = 0; i < ret.lin; i++)
                for (int j = 0; j < ret.col; j++)
                    ret._matrix[j, i] = ret._matrix[i, j];
            return ret;
        }
        

        ///Sum of the principal diagonal
        public double TraceM(Matrix a)
        {
            double ret;
            int i;
            ret = 0;
            for (i = 1; i <= a.lin; i++)
                ret += a._matrix[i, i];
            return ret;
        }

        
        #endregion   

        public List<Matrix> partitioningMatrixInFour(Matrix a, int p)
        {
            List<Matrix> matrixList = new List<Matrix>();
            Matrix a1 = new Matrix(p, p), a2 = new Matrix(a.lin - p, p), a3 = new Matrix(p, a.col - p),
                    a4 = new Matrix(a.lin - p, a.col - p);
            for (int i = 1; i <= p; i++)
                for (int j = 1; j <= p; j++)
                    a1._matrix[i,j] = a._matrix[i,j];
            matrixList.Add(a1);

            for (int i = p + 1; i <= a.lin; i++)
                for (int j = 1; j <= p; j++)
                    a2._matrix[i - p,j] = a._matrix[i,j];
            matrixList.Add(a2);

            for (int i = 1; i <= p; i++)
                for (int j = p + 1; j <= a.col; j++)
                    a3._matrix[i,j - p] = a._matrix[i,j];
            matrixList.Add(a3);

            for (int i = p + 1; i <= a.lin; i++)
                for (int j = p + 1; j <= a.col; j++)
                    a4._matrix[i - p,j - p] = a._matrix[i,j];
            matrixList.Add(a4);
            return matrixList;
        }

        public Matrix buildMatrixFromFourMatrices(Matrix b1, Matrix b2, Matrix b3, Matrix b4)
        {
            Matrix B = new Matrix(b1.lin + b2.lin, b1.col + b3.col);
            for (int i = 1; i <= b1.lin; i++)
                for (int j = 1; j <= b1.col; j++)
                    B._matrix[i,j] = b1._matrix[i,j];

            for (int i = b1.lin + 1; i <= B.lin; i++)
                for (int j = 1; j <= b2.col; j++)
                    B._matrix[i,j] = b2._matrix[i - b1.lin,j];

            for (int i = 1; i <= b3.lin; i++)
                for (int j = b1.col + 1; j <= B.col; j++)
                    B._matrix[i,j] = b3._matrix[i,j - b1.col];

            for (int i = b1.lin + 1; i <= B.col; i++)
                for (int j = b1.lin + 1; j <= B.col; j++)
                    B._matrix[i,j] = b4._matrix[i - b1.lin,j - b1.col];
            return B;
        }

        public Matrix product(double number, Matrix a, int l)
        {
            Matrix ret = a.Clone();
            for (int j = 1; j <= ret.col; j++)
                ret._matrix[l,j] = number * a._matrix[l,j];
            ret = rounding(ret, 2);
            return ret;
        }
        
        public Matrix addL1toL2(Matrix a, int l1, int l2, double val)
        {
            Matrix b = a.Clone();
            for (int i = 1; i <= a.col; i++)
            {
                b._matrix[l2,i] += b._matrix[l1,i] * val;
            }
            b = rounding(b, 2);
            return b;
        }

        public Matrix addL1toL2(Matrix a, int l1, int l2)
        {
            Matrix b = a.Clone();
            for (int i = 1; i <= a.col; i++)
            {
                b._matrix[l2,i] += b._matrix[l1,i];
            }
            b = rounding(b, 2);
            return b;
        }

        public Matrix rounding(Matrix a, int precizie)
        {
            String format = "0.";
            for (int i = 1; i <= precizie; i++)
                format = format + "0";
            for (int i = 1; i <= a.lin; i++)
                for (int j = 1; j <= a.col; j++)
                {
                   // a._matrix[i,j] = Double.parseDouble((new DecimalFormat(format).format(a._matrix[i,j])).replace(',', '.'));
                }
            return a;
        }
        public bool Symmetric(Matrix a)
        {
            if (a.lin != a.col)
                return false;
            for (int i = 1; i < a.lin; i++)
                for (int j = i + 1; j <= a.col; j++)
                    if (a._matrix[i,j] != a._matrix[j,i])
                        return false;
            return true;
        }
        public Coordinates maxElem(Matrix a)
        {
            Coordinates coord = new Coordinates();
            coord.i = 2;
            coord.j = 1;
            coord.val = a._matrix[2,1];
            for (int i = 2; i <= a.lin; i++)
                for (int j = 1; j < i; j++)
                    if (a._matrix[i,j] > coord.val)
                    {
                        coord.i = i;
                        coord.j = j;
                        coord.val = a._matrix[i,j];
                    }
            return coord;
        }
    
        public Matrix deletelinL(int l, Matrix a)
        {
            int i, j;
            Matrix ret = new Matrix(a.lin - 1, a.col);
            for (i = 1; i <= l - 1; i++)
                for (j = 1; j <= ret.lin; j++)
                    ret._matrix[i,j] = a._matrix[i,j];
            for (i = l; i <= ret.lin; i++)
                for (j = 1; j <= ret.col; j++)
                    ret._matrix[i,j] = a._matrix[i + 1,j];
            return ret;
        }

        public Matrix deleteColC(int c, Matrix a)
        {
            int i, j;
            Matrix ret = new Matrix(a.lin, a.col - 1);
            for (i = 1; i <= ret.lin; i++)
                for (j = 1; j <= c - 1; j++)
                    ret._matrix[i,j] = a._matrix[i,j];
            for (i = 1; i <= ret.col; i++)
                for (j = c; j <= ret.col; j++)
                    ret._matrix[i,j] = a._matrix[i,j + 1];
            return ret;
        }
        public double Det(Matrix a)
        {
            int poz, i, j, k, semn;
            double aux, pivot, max, ret;
            semn = 0;
            ret = 0;
            Matrix b = new Matrix(a.lin, a.col);
            for (i = 1; i <= b.lin; i++)
                for (j = 1; j <= b.col; j++)
                    b._matrix[i,j] = a._matrix[i,j];
            for (i = 1; i <= b.lin; i++)
            {
                max = Math.Abs(b._matrix[i,i]);
                poz = i;
                for (j = i + 1; j <= b.lin; j++)
                    if (Math.Abs(b._matrix[j,i]) > max)
                    {
                        max = Math.Abs(b._matrix[j,i]);
                        poz = j;
                    }
                if (max == 0)
                    return ret;
                else
                {
                    if (poz != i)
                    {
                        for (j = 1; j <= b.col; j++)
                        {
                            aux = b._matrix[i,j];
                            b._matrix[i,j] = b._matrix[poz,j];
                            b._matrix[poz,j] = aux;
                        }
                        semn++;
                    }
                }
                for (j = i + 1; j <= b.lin; j++)
                {
                    pivot = b._matrix[j,i];
                    for (k = i; k <= b.col; k++)
                        b._matrix[j,k] = b._matrix[j,k] - pivot / b._matrix[i,i] * b._matrix[i,k];
                }

            }
            ret = 1;
            for (i = 1; i <= b.lin; i++)
                ret *= b._matrix[i,i];
            if (semn % 2 == 1)
                ret = -ret;
            return ret;
        }

        public int Rank(Matrix a)
        {
            int i, j, k, ret, poz, poz1;
            double aux, pivot, max, max1;
            Matrix b = new Matrix(a.lin, a.col);
            for (i = 1; i <= b.lin; i++)
                for (j = 1; j <= b.col; j++)
                    b._matrix[i,j] = a._matrix[i,j];
            if (b.lin > b.col)
                b = Transpose(b);
            for (i = 1; i <= b.lin; i++)
            {
                max1 = Math.Abs(b._matrix[i,i]);
                poz1 = i;
                for (j = i + 1; j <= b.col; j++)
                    if (Math.Abs(b._matrix[i,j]) > max1)
                    {
                        max1 = Math.Abs(b._matrix[i,j]);
                        poz1 = j;
                    }
                if (max1 != 0)
                    for (j = 1; j <= b.lin; j++)
                    {
                        aux = b._matrix[j,i];
                        b._matrix[j,i] = b._matrix[j,poz1];
                        b._matrix[j,poz1] = aux;
                    }
                max = Math.Abs(b._matrix[i,i]);
                poz = i;
                for (j = i + 1; j <= b.lin; j++)
                    if (Math.Abs(b._matrix[j,i]) > max)
                    {
                        max = Math.Abs(b._matrix[j,i]);
                        poz = j;
                    }
                if (max != 0)
                {
                    if (poz != i)
                        for (j = 1; j <= b.col; j++)
                        {
                            aux = b._matrix[i,j];
                            b._matrix[i,j] = b._matrix[poz,j];
                            b._matrix[poz,j] = aux;
                        }
                    for (j = i + 1; j <= b.lin; j++)
                    {
                        pivot = b._matrix[j,i];
                        for (k = i; k <= b.col; k++)
                            b._matrix[j,k] = b._matrix[j,k] - pivot / b._matrix[i,i] * b._matrix[i,k];
                    }
                    for (j = i + 1; j <= b.col; j++)
                    {
                        pivot = b._matrix[i,j];
                        for (k = i; k <= b.lin; k++)
                            b._matrix[k,j] = b._matrix[k,j] - pivot / b._matrix[i,i] * b._matrix[k,i];
                    }
                }
            }
            ret = 0;
            for (i = 1; i <= b.lin; i++)
                ret += (b._matrix[i,i] != 0 ? 1 : 0);
            return ret;
        }

        public Matrix Inverse(Matrix a)
        {
            Matrix aextins = new Matrix(a.lin, 2 * a.col), ret = new Matrix(a.lin, a.col);
            double max, pivot, aux;
            int i, j, k, poz;
            for (i = 1; i <= a.lin; i++)
                for (j = 1; j <= a.col; j++)
                    aextins._matrix[i,j] = a._matrix[i,j];
            for (i = 1; i <= aextins.lin; i++)
                for (j = a.col + 1; j <= aextins.col; j++)
                    aextins._matrix[i,j] = (j - i == a.col ? 1 : 0);
            for (i = 1; i <= aextins.lin; i++)
            {
                max = Math.Abs(aextins._matrix[i,i]);
                poz = i;
                for (j = i + 1; j <= aextins.lin; j++)
                    if (Math.Abs(aextins._matrix[j,i]) > max)
                    {
                        max = Math.Abs(aextins._matrix[j,i]);
                        poz = j;
                    }
                if (max <= 1e-7)
                {
                    Console.Write("\033[H\033[2J");
                    Console.WriteLine("Singular matrix !!!");
                    System.Environment.Exit(1);
                }
                if (poz != i)
                    for (k = i; k <= aextins.col; k++)
                    {
                        aux = aextins._matrix[poz,k];
                        aextins._matrix[poz,k] = aextins._matrix[i,k];
                        aextins._matrix[i,k] = aux;
                    }
                pivot = aextins._matrix[i,i];
                for (j = i; j <= aextins.col; j++)
                    aextins._matrix[i,j] = aextins._matrix[i,j] / pivot;
                for (j = 1; j <= aextins.lin; j++)
                    if (j != i)
                    {
                        pivot = aextins._matrix[j,i];
                        for (k = i; k <= aextins.col; k++)
                            aextins._matrix[j,k] = aextins._matrix[j,k] - pivot * aextins._matrix[i,k];
                    }
            }
            for (i = 1; i <= ret.lin; i++)
                for (j = 1; j <= ret.col; j++)
                    ret._matrix[i,j] = aextins._matrix[i,j + a.col];
            return ret;
        }

        public Matrix product(double number, Matrix a)
        {
            Matrix ret = new Matrix(a.lin, a.col);
            int i, j;
            for (i = 1; i <= ret.lin; i++)
                for (j = 1; j <= ret.col; j++)
                    ret._matrix[i,j] = number * a._matrix[i,j];
            return ret;
        }

        public double norma_l(Matrix a)
        {
            double ret, suma;
            int i, j;
            ret = 0;
            for (i = 1; i <= a.lin; i++)
                ret += Math.Abs(a._matrix[i,1]);
            for (j = 2; j <= a.col; j++)
            {
                suma = 0;
                for (i = 1; i <= a.lin; i++)
                    suma += Math.Abs(a._matrix[i,j]);
                if (suma > ret)
                    ret = suma;
            }
            return ret;
        }

        public double norma_inf(Matrix a)
        {
            double ret, suma;
            int i, j;
            ret = 0;
            for (i = 1; i <= a.col; i++)
                ret += Math.Abs(a._matrix[1,i]);
            for (i = 2; i <= a.lin; i++)
            {
                suma = 0;
                for (j = 1; j <= a.col; j++)
                    suma += Math.Abs(a._matrix[i,j]);
                if (suma > ret)
                    ret = suma;
            }
            return ret;
        }

        public double norma_k(Matrix a)
        {
            double ret;
            int i, j;
            ret = 0;
            for (i = 1; i <= a.lin; i++)
                for (j = 1; j <= a.col; j++)
                    ret += Math.Abs(Math.Pow(a._matrix[i,j], 2.0));
            ret = Math.Pow(ret, 1.0 / 2.0);
            return ret;
        }

        public Matrix resolvingSystem(Matrix a, Matrix b)
        {
            Matrix aextins = new Matrix(a.lin, a.col + 1);
            double max, aux, pivot;
            int i, j, k, poz;
            for (i = 1; i <= aextins.lin; i++)
                for (j = 1; j < aextins.col; j++)
                    aextins._matrix[i,j] = a._matrix[i,j];
            for (i = 1; i <= aextins.lin; i++)
                aextins._matrix[i,aextins.col] = b._matrix[i,1];
            for (i = 1; i <= aextins.lin; i++)
            {
                max = Math.Abs(aextins._matrix[i,i]);
                Console.WriteLine("Max " + i + " " + max);
                poz = i;
                for (j = i + 1; j <= aextins.lin; j++)
                    if (Math.Abs(aextins._matrix[j,i]) > max)
                    {
                        max = Math.Abs(aextins._matrix[j,i]);
                        Console.WriteLine("Max " + i + " " + j + " " + max);
                        poz = j;
                    }
                if (max == 0)
                {
                    Console.WriteLine("The matrix of the system is singular");
                    System.Environment.Exit(1);
                }
                if (poz != i)
                    for (j = 1; j <= aextins.col; j++)
                    {
                        aux = aextins._matrix[i,j];
                        aextins._matrix[i,j] = aextins._matrix[poz,j];
                        aextins._matrix[poz,j] = aux;
                    }
                for (j = i + 1; j <= aextins.lin; j++)
                {
                    pivot = aextins._matrix[j,i];
                    for (k = i; k <= aextins.col; k++)
                        aextins._matrix[j,k] = aextins._matrix[j,k] - aextins._matrix[i,k] * pivot / aextins._matrix[i,i];
                }
            }
            Matrix ret = new Matrix(aextins.lin, 1);
            for (i = ret.lin; i >= 1; i--)
            {
                aux = 0;
                for (j = ret.lin; j > i; j--)
                    aux += aextins._matrix[i,j] * ret._matrix[j,1];
                ret._matrix[i,1] = 1.0 / aextins._matrix[i,i] * (aextins._matrix[i,aextins.col] - aux);
            }
            return ret;
        }

        public Matrix raiseTheMatrixToPower(Matrix a, int k)
        {
            Matrix ret = new Matrix(a.lin, a.col);
            int i, j;
            for (i = 1; i <= a.lin; i++)
                for (j = 1; j <= a.col; j++)
                    ret._matrix[i,j] = (i == j) ? 1 : 0;
            for (i = 1; i <= k; i++)
                ret = MultiplyM(ret, a);
            return ret;
        }
        
        public Partition partition_d(Matrix a)
        {
            Partition ret = new Partition(a.lin, a.lin);
            int i, j, k;
            double sum;
            for (i = 1; i <= ret.L.lin; i++)
                for (j = 1; j <= ret.L.col; j++)
                    ret.L._matrix[i,j] = ret.U._matrix[i,j] = 0;
            for (i = 1; i <= ret.L.lin; i++)
                ret.L._matrix[i,i] = 1;
            for (i = 1; i <= ret.U.col; i++)
                ret.U._matrix[1,i] = a._matrix[1,i];
            for (i = 2; i <= ret.L.lin; i++)
            {
                for (j = 1; j < i; j++)
                {
                    sum = 0;
                    for (k = 1; k < j; k++)
                        sum += ret.L._matrix[i,k] * ret.U._matrix[k,j];
                    ret.L._matrix[i,j] = (a._matrix[i,j] - sum) / ret.U._matrix[j,j];
                }
                for (j = i; j <= ret.U.col; j++)
                {
                    sum = 0;
                    for (k = 1; k < i; k++)
                        sum += ret.L._matrix[i,k] * ret.U._matrix[k,j];
                    ret.U._matrix[i,j] = a._matrix[i,j] - sum;
                }
            }
            return ret;
        }

        public Partition partition_c(Matrix a)
        {
            Partition ret = new Partition(a.lin, a.lin);
            int i, j, k;
            double sum;
            for (i = 1; i <= ret.L.lin; i++)
                for (j = 1; j <= ret.L.col; j++)
                    ret.L._matrix[i,j] = ret.U._matrix[i,j] = 0;
            for (i = 1; i <= ret.U.lin; i++)
                ret.U._matrix[i,i] = 1;
            for (i = 1; i <= ret.L.lin; i++)
                ret.L._matrix[i,1] = a._matrix[i,1];
            for (i = 2; i <= ret.U.col; i++)
                ret.U._matrix[1,i] = a._matrix[1,i] / ret.L._matrix[1,1];
            for (i = 2; i <= ret.L.lin; i++)
            {
                for (j = 2; j <= i; j++)
                {
                    sum = 0;
                    for (k = 1; k < i; k++)
                        sum += ret.L._matrix[i,k] * ret.U._matrix[k,j];
                    ret.L._matrix[i,j] = a._matrix[i,j] - sum;
                }
                for (j = i + 1; j <= ret.U.col; j++)
                {
                    sum = 0;
                    for (k = 1; k <= i; k++)
                        sum += ret.L._matrix[i,k] * ret.U._matrix[k,j];
                    ret.U._matrix[i,j] = (a._matrix[i,j] - sum) / ret.L._matrix[i,i];
                }
            }
            return ret;
        }


        public Partition partition_cholesky(Matrix a)
        {
            Partition ret = new Partition(a.lin, a.lin);
            int i, j, k;
            double suma;
            for (i = 1; i <= ret.U.lin; i++)
                for (j = 1; j <= ret.U.col; j++)
                    ret.U._matrix[i,j] = 0;
            for (i = 1; i <= ret.U.lin; i++)
            {
                suma = 0;
                for (j = 1; j < i; j++)
                    suma += Math.Pow(ret.U._matrix[j,i], 2);
                ret.U._matrix[i,i] = Math.Pow(a._matrix[i,i] - suma, 1.0 / 2);
                for (j = i + 1; j <= ret.U.col; j++)
                {
                    suma = 0;
                    for (k = 1; k < i; k++)
                        suma += ret.U._matrix[k,i] * ret.U._matrix[k,j];
                    ret.U._matrix[i,j] = 1.0 / ret.U._matrix[i,i] * (a._matrix[i,j] - suma);
                }
            }
            ret.L = Transpose(ret.U);
            return ret;
        }

        public Factorization factorizare_lr(Matrix a)
        {
            Partition aux;
            aux = partition_d(a);
            Factorization ret = new Factorization(aux);
            return ret;
        }

        public double norma(Vector a)
        {
            double ret;
            ret = 0;
            int i;
            for (i = 1; i <= a.lin; i++)
                ret += Math.Pow(a._matrix[i], 2);
            ret = Math.Pow(ret, 0.5);
            return ret;
        }

        public Vector sum(Vector a, Vector b)
        {
            Vector ret = new Vector(nmax);
            ret.lin = a.lin;
            int i;
            for (i = 1; i <= ret.lin; i++)
                ret._matrix[i] = a._matrix[i] + b._matrix[i];
            return ret;
        }

        public Vector difference(Vector a, Vector b)
        {
            Vector ret = new Vector(nmax);
            ret.lin = a.lin;
            int i;
            for (i = 1; i <= ret.lin; i++)
                ret._matrix[i] = a._matrix[i] - b._matrix[i];
            return ret;
        }

        public Vector product(Matrix a, Vector b)
        {
            Vector ret = new Vector(nmax);
            int i, j;
            ret.lin = b.lin;
            for (i = 1; i <= ret.lin; i++)
            {
                ret._matrix[i] = 0;
                for (j = 1; j <= a.col; j++)
                    ret._matrix[i] += a._matrix[i,j] * b._matrix[j];
            }
            return ret;
        }

        public double norma_inf(Vector a)
        {
            double ret;
            int i;
            ret = Math.Abs(a._matrix[1]);
            for (i = 2; i <= a.lin; i++)
                if (Math.Abs(a._matrix[i]) > ret)
                    ret = Math.Abs(a._matrix[i]);
            return ret;
        }

        public Vector conversion(Matrix x)
        {
            Vector ret = new Vector(nmax);
            int i;
            ret.lin = x.lin;
            for (i = 1; i <= ret.lin; i++)
                ret._matrix[i] = x._matrix[i,1];
            return ret;
        }

        public double product(Vector a, Vector b)
        {
            double ret;
            int i;
            ret = 0;
            for (i = 1; i <= a.lin; i++)
                ret += a._matrix[i] * b._matrix[i];
            return ret;
        }

        public Vector product(double number, Vector a)
        {
            Vector ret = new Vector(nmax);
            int i;
            ret.lin = a.lin;
            for (i = 1; i <= ret.lin; i++)
                ret._matrix[i] = number * a._matrix[i];
            return ret;
        }


    }
}
