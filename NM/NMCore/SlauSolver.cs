using System;

namespace IvanovAC.NM
{
    /// <summary>
    /// Общий предок классов, реализующих методы решения
    /// систем линейных алгебраических уравнений (СЛАУ).
    /// </summary>
    public abstract class SlauSolver
    {
        /// <summary>
        /// Решает СЛАУ.
        /// </summary>
        /// <param name="a">Матрица коэффициентов.</param>
        /// <param name="b">Правая часть.</param>
        /// <returns>Вектор неизвестных - решение СЛАУ.</returns>
        public abstract double[] Solve(double[,] a, double[] b);

        /// <summary>
        /// Допустимая относительная погрешность вычислений (невязка).
        /// </summary>
        public double RequiredRelativeError
        {
            get
            {
                return this.requiredRelativeError;
            }
            set
            {
                if (value > 0)
                {
                    this.requiredRelativeError = value;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        /// <summary>
        /// Фактическая относительная погрешность вычислений (невязка).
        /// </summary>
        public double RelativeError
        {
            get
            {
                return this.relativeError;
            }
        }
        /// <summary>
        /// Максимально возможное число итераций, при достижении которого решение СЛАУ будет прервано.
        /// </summary>
        public int MaxIterationCount
        {
            get
            {
                return this.maxIterationCount;
            }
            set
            {
                if (value > 0)
                {
                    this.maxIterationCount = value;
                }
                else
                {
                    throw new Exception();
                }
                
            }
        }
        /// <summary>
        /// Число итераций, выполненное в процессе решения СЛАУ.
        /// </summary>
        public int IterationCount
        {
            get
            {
                return this.iterationCount;
            }
            set
            {
                iterationCount = value;
            }
        }

        /// <summary>
        /// Возвращает ошибки решения
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                errorMessage = value;
            }
        }

        protected double requiredRelativeError;
        protected double relativeError;
        protected int maxIterationCount;
        protected int iterationCount;
        protected string errorMessage;

        /// <summary>
        /// Перемножает матрицу и вектор
        /// </summary>
        /// <param name="A"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double[] MultMatrixVector(double[,] A, double[] x)
        {            
            int n = A.GetLength(0);
            double[] b = new double[n];

            if (n != x.Length)
            {
                throw new Exception("Ошибка размерностей входных данных");
            }

            for(int i = 0; i < n; i++)
            {
                double tmp = 0;
                for(int j = 0; j < n; j++)
                {
                    tmp += A[i, j] * x[j];
                }
                b[i] = tmp;
            }

            return b;
        }

        /// <summary>
        /// Вычисляет скалярное произведение
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double ScalarProduct(double[] x, double[] y)
        {
            int n = x.Length;
            double result = 0;

            if (n != y.Length)
            {
                throw new Exception("Ошибка размерностей входных данных");
            }

            for (int i = 0; i < n; i++)            
                result += x[i] * y[i];            
            
            return result;
        }

        /// <summary>
        /// Перемножает матрицу и число
        /// </summary>
        /// <param name="A"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double[] MultMatrixValue(double[] A, double x)
        {
            int n = A.Length;
            double[] result = new double[n];
            for(int i = 0; i < n; i++)            
                result[i] = A[i] * x;
            
            return result;
        }

        /// <summary>
        /// Складывает два вектора
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double[] AddVector(double[] x, double[] y)
        {
            int n = x.Length;
            double[] result = new double[n];

            if (n != y.Length)
            {
                throw new Exception("Ошибка размерностей входных данных");
            }

            for (int i = 0; i < n; i++)            
                result[i] = x[i] + y[i];
            
            return result;
        }

        /// <summary>
        /// Вычитает первый вектор из второго
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static double[] SubVector(double[] x, double[] y)
        {
            int n = x.Length;
            double[] result = new double[n];

            if (n != y.Length)
            {
                throw new Exception("Ошибка размерностей входных данных");
            }

            for (int i = 0; i < n; i++)
                result[i] = x[i] - y[i];

            return result;
        }

        /// <summary>
        /// Вычисляет норму вектора
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Norm(double[] x)
        {
            return Math.Sqrt(ScalarProduct(x, x));
        }
    }
}
