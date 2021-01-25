using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaCasa.Entities
{
    public class Partition
    {
        public Matrix L;
        public Matrix U;

        public Partition(int lin, int col)
        {
            L = new Matrix(lin, col);
            U = new Matrix(lin, col);
        }
        public Partition(Matrix L, Matrix U)
        {
            this.L = L;
            this.U = U;
        }
    }
}
