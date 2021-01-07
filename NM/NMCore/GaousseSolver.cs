namespace IvanovAC.NM
{
    /// <summary>
    /// Метод Гаусса решения СЛАУ.
    /// </summary>
    public class GaousseSolver : SlauSolver
    {
        /// <summary>
        /// Решает СЛАУ.
        /// </summary>
        /// <param name="a">Матрица коэффициентов.</param>
        /// <param name="b">Правая часть.</param>
        /// <returns>Вектор неизвестных - решение СЛАУ.</returns>
        public override double[] Solve(double[,] a, double[] b)
        {
            int n = a.GetLength(0);
            for (int k = 0; k < n; k++)
            {
                double akk = a[k,k];

                if (akk != 0)
                {
                    b[k] /= akk;
                    for(int j = k; j < n; j++)                    
                        a[k, j] /= akk;                    
                }

                for(int i = k +1; i < n; i++)
                {
                    double aik = a[i, k];
                    if (aik != 0)
                    {
                        b[i] /= aik;
                        b[i] -= b[k];
                        for(int j = k; j < n; j++)
                        {
                            a[i, j] /= aik;
                            a[i, j] -= a[k, j];
                        }
                    }
                }                
            }

            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i + 1; j < n; j++)
                    b[i] -= a[i, j] * b[j];
            }

            return b;
        }
    }
}
