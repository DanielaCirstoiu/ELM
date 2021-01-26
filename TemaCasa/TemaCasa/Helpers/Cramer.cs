using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa.Helpers
{
    public class Cramer
    {

        Matrix A;
        double[] B;
        int size;

        public Cramer()
        {
            A = null;
            B = null;
            size = 0;
        }

        public Cramer(Matrix m2, double[] x)
        {
            this.A = m2;
            this.B = x;
            this.size = x.Length;
        }

        public double[] solve()
        {
            Matrix tempMatrix = new Matrix(size,size);
            double[] x = new double[size];
            double detCohef = this.calculateCoeficientsMatrixdeterminant();
            for (int i = 0; i < size; i++)
            {
                for (int k = 0; k < size; k++)
                {
                    for (int l = 0; l < size; l++)
                    {
                        tempMatrix._matrix[k,l] = A._matrix[k,l];
                    }
                }
                for (int k = 0; k < size; k++)
                {
                    tempMatrix._matrix[k,i] = B[k];
                }
                double det = this.CalcDet(tempMatrix, size);
                x[i] = det / detCohef;
            }
            return x;
        }

        public double calculateCoeficientsMatrixdeterminant()
        {
            return CalcDet(this.A, this.size);
        }

        public double CalcDet(Matrix m, int sizeM)
        {
            if (sizeM == 1)
            {
                return m._matrix[0,0];
            }
            else if (sizeM == 2)
            {
                return m._matrix[0,0] * m._matrix[1,1] - m._matrix[0,1] * m._matrix[1,0];
            }
            else
            {
                double sum = 0.0;
                int multiplier = -1;
                for (int i = 0; i < sizeM; i++)
                {
                    multiplier = multiplier == -1 ? 1 : -1;
                    Matrix nM = this.copyMatrix(m, sizeM, i);
                    double det = multiplier * m._matrix[0,i] * CalcDet(nM, sizeM - 1);
                    sum += det;
                }
                return sum;
            }
        }

        public Matrix copyMatrix(Matrix m, int size, int col)
        {
            int sizeM =size-1;
            Matrix result = new Matrix(sizeM,sizeM);
            int nI = 0;
            for (int i = 1; i < size; i++)
            {
                int nJ = 0;
                for (int j = 0; j < size; j++)
                {
                    if (j != col)
                    {
                        result._matrix[nI,nJ] = m._matrix[i,j];
                        nJ++;
                    }
                }
                nI++;
            }
            return result;
        }

    }
}
