using System;

namespace IvanovAC.NM
{
    /// <summary>
    /// Полиномиальная базисная функция
    /// </summary>
    public class FirstTypePolynomialBasisFunction : IBasisFunction
    {
        /// <summary>
        /// Базисная функция для граничного условия первого рода.
        /// </summary>
        /// <param name="a">Левый конец отрезка.</param>
        /// <param name="b">Правый конец отрезка.</param>
        /// <param name="ya">Значение функции в точке a.</param>
        /// <param name="yb">Значение функции в точке b.</param>
        /// <param name="j">Степень полинома</param>
        public FirstTypePolynomialBasisFunction(double a, double b, double ya,
        double yb, int j)
        {
            this.a = a;
            this.b = b;
            this.ya = ya;
            this.yb = yb;
            this.j = j;
        }
        /// <summary>
        /// Возвращает значение базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>       
        /// <returns>Значение базисной функции.</returns>
        public double Function(double x)
        {
            return Math.Pow(x - a, j)*(b - x);
        }
        /// <summary>
        /// Возвращает значение первой производной базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение первой производной базисной функции.</returns>
        public double FirstDerivative(double x)
        {
            return j * Math.Pow(x - a, j - 1) * (b - x) - Math.Pow(x - a, j);
        }
        /// <summary>
        /// Возвращает значение второй производной базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение второй производной базисной функции.</returns>
        public double SecondDerivative(double x)
        {
            return j * (j - 1) * Math.Pow(x - a, j - 2) * (b - x) - 2 * j * Math.Pow(x - a, j - 1);
        }
        double a;
        double b;
        double ya;
        double yb;
        int j;
    }
}
