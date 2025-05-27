using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlarCalculator.Solvers
{
    internal class KholetskySolver
    {
        private static void Factorize(double[,] a)
        {
            int n = a.GetLength(0);
            if (n != a.GetLength(1))
                throw new ArgumentException("Матриця повинна бути квадратной", nameof(a));

            const double eps = 1e-12;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (Math.Abs(a[i, j] - a[j, i]) > eps)
                        throw new ArgumentException("Матриця повинна бути симетричною", nameof(a));


            for (int j = 0; j < n; j++)
            {
                double sumDiag = 0.0;
                for (int k = 0; k < j; k++)
                {
                    double l_jk = a[j, k];
                    sumDiag += l_jk * l_jk;
                }

                double diag = a[j, j] - sumDiag;
                if (diag <= 0.0 || double.IsNaN(diag))
                    throw new ArgumentException("Повинна бути додатноозначена матриця", nameof(a));

                a[j, j] = Math.Sqrt(diag);
                double invLjj = 1.0 / a[j, j];

                for (int i = j + 1; i < n; i++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < j; k++)
                        sum += a[i, k] * a[j, k];

                    a[i, j] = (a[i, j] - sum) * invLjj;
                }
            }
        }

        public double[] Solve(double[,] a, IReadOnlyList<double> b)
        {
            int n = a.GetLength(0);
            if (n != a.GetLength(1))
                throw new ArgumentException("Матриця повинна бути квадратной.", nameof(a));
            if (b.Count != n)
                throw new ArgumentException("Розмір вектора B не співпадає з розміром матриці", nameof(b));


            var l = (double[,])a.Clone();

            Factorize(l);


            var y = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0.0;
                for (int k = 0; k < i; k++)
                    sum += l[i, k] * y[k];

                y[i] = (b[i] - sum) / l[i, i];
            }

            var x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0.0;
                for (int k = i + 1; k < n; k++)
                    sum += l[k, i] * x[k];

                x[i] = (y[i] - sum) / l[i, i];
            }

            return x;
        }
    }
}
