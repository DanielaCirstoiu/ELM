using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa.Helpers
{
    public class Horner
    {

        private Vector res;


        public Vector getRes()
        {
            return res;
        }
        public void calculate(double x, Vector p)
        {
            double result = 0;
            Vector res = new Vector(p.lin);
            for (int i = p.lin- 1; i >= 0; i--)
            {
                result = p._matrix[i] + (x * result);
                res._matrix[p.lin - i - 1] = result;
            }
        }

        public double eval(double x, Vector p)
        {
            double result = 0;
            for (int i = p.lin - 1; i >= 0; i--)
            {
                result = p._matrix[i] + (x * result);
            }

            return result;
        }

    }
}
