using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlarCalculator.Solvers
{
    internal class GaussSolver
    {
        /// <summary>
        /// Решает систему линейных алгебраических уравнений методом Гаусса.
        /// </summary>
        /// <param name="matrix">Матрица коэффициентов [n, n]</param>
        /// <param name="freeMembers">Вектор свободных членов [n]</param>
        /// <returns>Вектор решения x[n]</returns>
        public decimal[] Solve(decimal[,] matrix, decimal[] freeMembers)
        {
            int n = freeMembers.Length;
            decimal[,] a = (decimal[,])matrix.Clone();
            decimal[] b = (decimal[])freeMembers.Clone();
            decimal[] x = new decimal[n];

            // Прямой ход
            for (int i = 0; i < n; i++)
            {
                // Поиск максимального элемента в столбце i
                int maxRow = i;
                for (int k = i + 1; k < n; k++)
                {
                    if (Math.Abs(a[k, i]) > Math.Abs(a[maxRow, i]))
                        maxRow = k;
                }

                // Перестановка строк
                for (int k = i; k < n; k++)
                {
                    decimal temp = a[maxRow, k];
                    a[maxRow, k] = a[i, k];
                    a[i, k] = temp;
                }
                decimal tempB = b[maxRow];
                b[maxRow] = b[i];
                b[i] = tempB;

                // Нормализация строки
                decimal diag = a[i, i];
                if (diag == 0)
                    throw new InvalidOperationException("Матриця несовместна");
                for (int k = i; k < n; k++)
                    a[i, k] /= diag;
                b[i] /= diag;

                // Обнуление ниже текущей строки
                for (int j = i + 1; j < n; j++)
                {
                    decimal factor = a[j, i];
                    for (int k = i; k < n; k++)
                        a[j, k] -= factor * a[i, k];
                    b[j] -= factor * b[i];
                }
            }

            // Обратный ход
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = b[i];
                for (int j = i + 1; j < n; j++)
                    x[i] -= a[i, j] * x[j];
            }

            return x;
        }
    }
}