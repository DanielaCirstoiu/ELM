using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa.Helpers
{
    public class Helper
    {
        public static bool IsSquare(Matrix m)
        {
            return m.lin == m.col;
        }

        public static Matrix RemoveRowAndColumn(Matrix m, int row, int column)
        {
            Matrix output = null;

            output = new Matrix(m.lin - 1, m.col - 1);
            int x = 0, y = 0;
            for (int i = 0; i < m.lin; i++)
            {
                y = 0;
                if (i == row)
                {
                    continue;
                }

                for (int j = 0; j < m.lin; j++)
                {
                    if (j == column)
                    {
                        continue;
                    }

                    output._matrix[x,y] = m._matrix[i,j];
                    y++;
                }
                x++;
            }

            return output;
        }
        public static int ElementSign(int i, int j)
        {
            int sign = ((i + j) % 2 == 0) ? 1 : -1;
            return sign;
        }

        public static bool IsZero(double determinant)
        {
            return determinant != 0;
        }

        public static float Pow(float x, int exp)
        {
            float result = 1.0f;
            if (exp == 0)
                return 1;
            if (exp < 0)
            {
                return 1 / Pow(x, Math.Abs(exp));
            }
            while (exp > 0)
            {
                if (exp % 2 == 1)
                    result *= x;
                exp >>= 1;
                x *= x;
            }

            return result;
        }

        public static double RoundNumber(double nr)
        {
            return Math.Round(nr, 0);
        }
    }
}
