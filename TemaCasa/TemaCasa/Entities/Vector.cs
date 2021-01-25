using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaCasa.Entities
{

    public class Vector
    {
        public int lin;
        public double[] _matrix;

        public Vector(int lin)
        {
            this.lin = lin;
            _matrix = new double[lin + 1];
        }

        public Vector Clone()
        {
            Vector clone = new Vector(lin);
            for (int i = 1; i <= lin; i++)
            {
                clone._matrix[i] = this._matrix[i];
            }
            return clone;
        }
    }

}
