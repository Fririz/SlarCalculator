using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlarCalculator.Solvers
{   
    internal class RotationSolver
    {
        public decimal[] Solve(decimal[,] A, decimal[] B)
        {
            return SolveUsingGivensRotation(A, B);
        }

        decimal[] SolveUsingGivensRotation(decimal[,] A, decimal[] B)
        {
            int size = A.GetLength(0);
            decimal[,] matrix = new decimal[size, size + 1];


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = A[i, j];
                }
                matrix[i, size] = B[i];
            }

            for (int i = 0; i < size - 1; i++)
            {
                for (int j = i + 1; j < size; j++)
                {
                    ApplyGivens(matrix, size, i, j);
                }
            }

            decimal[] result = BackSubstitution(matrix, size);
            return result;
        }

        void ApplyGivens(decimal[,] matrix, int size, int i, int j)
        {
            decimal a = matrix[i, i];
            decimal b = matrix[j, i];

            if (b == 0) return;

            decimal r = Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(a * a + b * b)));
            decimal cos = a / r;
            decimal sin = -b / r;

            for (int k = i; k <= size; k++)
            {
                decimal tempI = cos * matrix[i, k] - sin * matrix[j, k];
                decimal tempJ = sin * matrix[i, k] + cos * matrix[j, k];
                matrix[i, k] = tempI;
                matrix[j, k] = tempJ;
            }
        }

        decimal[] BackSubstitution(decimal[,] matrix, int size)
        {
            decimal[] result = new decimal[size];

            for (int i = size - 1; i >= 0; i--)
            {
                decimal sum = 0;

                for (int j = i + 1; j < size; j++)
                {
                    sum += matrix[i, j] * result[j];
                }

                result[i] = (matrix[i, size] - sum) / matrix[i, i];
            }

            return result;
        }
    }
}
