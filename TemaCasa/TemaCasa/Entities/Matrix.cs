using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TemaCasa.Entities
{
    public class Matrix
    {
        #region private Properties
        private string _name;
        internal double[,] _matrix;
        private int _col;
        private int _lin;
        private static int max = 10;

        #endregion

        /// Initialize a new matrix object
        public Matrix(int lin, int col)
        {
            _matrix = new double[lin, col];
            _col = col;
            _lin = lin;
        }

        public Matrix(double[,] lin)
        {
            _matrix = lin;
            _lin = lin.GetLength(0);
            _col = lin.GetLength(1);
        }

        public Matrix(Matrix a)
        {
            _matrix = new double[a.lin, a.col];
            for (int i = 0; i < a.lin; i++)
                for (int j = 0; j < a.col; j++)
                    val(i, j, a.val(i, j));
            _lin = a.lin;
            _col = a.col;
        }

        #region Accessors

        /// Get value of a(lin,col)
        public double val(int lin, int col)
        {
            if (lin > _lin || col > _col)
                throw new ArgumentException("Indices outside the matrix", "lin,col");
            return _matrix[lin, col];

        }

        public void val(int lin, int col, double v)
        {
            _matrix[lin, col] = v;
        }

        #endregion

        #region Properties
        //linia
        public int lin
        {
            get { return _lin; }
        }
        //coloana
        public int col
        {
            get { return _col; }
        }

        public string Name { get => _name; set => _name = value; }
        #endregion
        // print matrix to standard output
        public string Show()
        {
            string result = "";
            for (int i = 0; i < lin; i++)
            {
                for (int j = 0; j < col; j++)
                    result += _matrix[i, j] + " ";
                result += "\n";
            }
            return result;
        }

        public string Print()
        {
            StringBuilder text = new StringBuilder();
            for (int i = 0; i < lin; i++)
            {
                text.Append("\n");
                for (int j = 0; j < col; j++)
                    text.Append(_matrix[i, j] + " ");
            }
            return text.ToString();
        }


        public bool Equals(Matrix a)
        {
            if (a.lin != this.lin || a.col != this.col) return false;
            for (int i = 1; i <= lin; i++)
                for (int j = 1; j <= col; j++)
                    if (a._matrix[i, j] != _matrix[i, j]) return false;
            return true;
        }

        public Matrix Clone()
        {
            Matrix clone = new Matrix(lin, col);
            for (int i = 0; i < lin; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    clone._matrix[i, j] = this._matrix[i, j];
                }
            }
            return clone;
        }

        //    public Matrix methodKrylov()
        //    {
        //        Matrix pc = det_pol_car(this);
        //        if (this.lin % 2 == 1)
        //            for (int i = 0; i <= pc.lin; i++)
        //                pc._matrix[i, 1] = -pc._matrix[i, 1];
        //        return pc;
        //    }

        //    public Matrix methodDanilevski()
        //    {
        //        Matrix pc = det_pol_car(this);
        //        if (this.lin % 2 == 1)
        //            for (int i = 0; i <= pc.lin; i++)
        //                pc._matrix[i, 1] = -pc._matrix[i, 1];
        //        return pc;
        //    }

        //    public Matrix methodLeverriere()
        //    {
        //        Matrix pc;
        //        double[] an; double[] s;
        //        pc = new Matrix(this.lin, 1);
        //        //a.lin = a.col = pc.lin = n;
        //        for (int i = 1; i <= this.lin; i++)
        //            an[1] = a;
        //        s[1] = TraceM(an[1]);
        //        pc._matrix[0, 1] = 1;
        //        pc._matrix[1, 1] = -TraceM(an[1]);
        //        for (int i = 2; i <= a.lin; i++)
        //        {
        //            an[i] = PowerM(a, i);
        //            s[i] = TraceM(an[i]);
        //            double aux = 0;
        //            for (j = 1; j < i; j++)
        //                aux += pc._matrix[j, 1] * s[i - j];
        //            aux += s[i];
        //            pc._matrix[i, 1] = -1.0 / i * aux;
        //        }
        //        if (pc.lin % 2 == 1)
        //            for (int i = 0; i <= pc.lin; i++)
        //                pc._matrix[i, 1] = -pc._matrix[i, 1];
        //        return pc;
        //    }

        //    public Matrix methodDirect()
        //    {
        //        Matrix a, x, y, y1;
        //        Matrix lambda;
        //        double max;
        //        a = new Matrix(this.lin, this.lin);
        //        y = new Matrix(this.lin, 1);
        //        x = new Matrix(1, this.lin);
        //        for (int i = 1; i <= this.lin; i++)
        //        {
        //            y._matrix[i, 1] = 1;
        //        }
        //        y1[1] = y;
        //        for (int i = 2; i <= max; i++)
        //        {
        //            y1[i] = this.MultiplyM(a, y1[i - 1]);
        //            y1[i].print(writer);
        //        }
        //        double lambdaRounded = 0;
        //        for (int i = 1; i <= this.lin; i++)
        //        {
        //            lambda[i] = y1[max]._matrix[i, 1] / y1[max - 1]._matrix[i, 1];
        //            lambdaRounded += lambda[i];
        //        }
        //        writer.write("Lambda " + lambdaRounded / n + '\n');
        //        lambdaRounded = Math.round(lambdaRounded / n);
        //        writer.write("Lambda rotunjit " + lambdaRounded + '\n');

        //        double norma = 0;
        //        for (int i = 1; i <= n; i++)
        //        {
        //            norma += y1[max]._matrix[i][1] * y1[max]._matrix[i][1];
        //        }
        //        norma = Math.Sqrt(norma);
        //        for (int i = 1; i <= this.lin; i++)
        //        {
        //            x._matrix[1, i] = y1[max]._matrix[i][1] / norma;

        //        }
        //        return x;
        //    }
        //}
    }
}
