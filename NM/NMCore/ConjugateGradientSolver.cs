namespace IvanovAC.NM
{
    public class ConjugateGradientSolver : SlauSolver
    {       
        /// <summary>
        /// Решает СЛАУ.
        /// </summary>
        /// <param name="A">Матрица коэффициентов.</param>
        /// <param name="b">Правая часть.</param>
        /// <returns>Вектор неизвестных - решение СЛАУ.</returns>
        public override double[] Solve(double[,] A, double[] b)
        {
            int n = b.Length;
            double e = requiredRelativeError;
            double[] x0 = new double[n];
            double[] r0 = new double[n];
            double[] p0 = new double[n];

            x0 = b;
            r0 = SubVector(b, MultMatrixVector(A, x0));            
            p0 = r0;            

            for(int k = 0; k < iterationCount; k++)
            {
                double step = ScalarProduct(r0, r0) / ScalarProduct(MultMatrixVector(A, p0), p0);
                double[] xk = AddVector(x0,MultMatrixValue(p0, step));
                double[] rk = SubVector(r0, MultMatrixValue(MultMatrixVector(A, p0), step));

                double s = Norm(rk) / Norm(b);
                if (Norm(rk) / Norm(b) <= e)
                {
                    relativeError = Norm(rk) / Norm(b);
                    maxIterationCount = k + 1;
                    return xk;
                }

                double w = ScalarProduct(rk, rk) / ScalarProduct(r0, r0);
                double[] pk = AddVector(rk, MultMatrixValue(p0, w));
                r0 = rk;
                p0 = pk;
                x0 = xk;
            }

            errorMessage = "Решение не было достигнуто";
            return x0;
        }
    }
}
