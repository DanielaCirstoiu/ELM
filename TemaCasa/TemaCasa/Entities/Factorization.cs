using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaCasa.Entities
{
    public class Factorization
    {
        public Matrix L;
        public Matrix R;

        public Factorization(int lin, int col)
        {
            L = new Matrix(lin, col);
            R = new Matrix(lin, col);
        }
        public Factorization(Partition a)
        {
            L = a.L.Clone();
            R = a.U.Clone();
        }
        public Factorization(Matrix L, Matrix R)
        {
            this.L = L;
            this.R = R;
        }
    }
}
