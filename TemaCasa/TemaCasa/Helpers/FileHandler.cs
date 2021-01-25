using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaCasa.Entities;

namespace TemaCasa
{
    class FileHandler
    {
        static string defaultReadFilePath = "in.txt";
        static string defaultWriteFilePath = "I_A_RED.txt";

        public static List<Matrix> readFileforMatrix(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(filePath);
            List<Matrix> matrices = new List<Matrix>();
            foreach(var line in lines)
            {
                //read name of Matrix
                string [] elements = line.Split(' ');
                string nameM = elements[0];

                //read dimension
                int dimension;
                Int32.TryParse(elements[1], out dimension);

                //read values
                if (dimension * dimension == (elements.Length - 2))
                {
                    Matrix M = new Matrix(dimension, dimension);
                    int k = 2;
                    for (int i = 2; i < elements.Length; i+=dimension)
                    {
                        int valElem;
                        
                        for (int j = 0; j < dimension; j++)
                        {
                            Int32.TryParse(elements[k], out valElem);
                            M.val((i - 2)/dimension, j, valElem);
                            k++;                           
                        }
                        
                    }
                    matrices.Add(M);
                }
            }
            return matrices;       
        }


        public static void writeFile(string filePath, string text)
        {
            System.IO.File.WriteAllText(filePath, text);
        }
   
    }
}
