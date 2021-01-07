using System;

namespace IvanovAC.NM
{
    /// <summary>
    /// Периодическая базисная функция
    /// </summary>
    public class FirstTypePeriodicBasisFunction : IBasisFunction
    {
        /// <summary>
        /// Базисная функция для граничного условия первого рода.
        /// </summary>
        /// <param name="a">Левый конец отрезка.</param>
        /// <param name="b">Правый конец отрезка.</param>
        /// <param name="ya">Значение функции в точке a.</param>
        /// <param name="yb">Значение функции в точке b.</param>
        /// <param name="j">Счетчик</param>
        public FirstTypePeriodicBasisFunction(double a, double b, double ya,
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
            return Math.Sin(Math.PI * j * (x - a) / (b - a));
        }
        /// <summary>
        /// Возвращает значение первой производной базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение первой производной базисной функции.</returns>
        public double FirstDerivative(double x)
        {
            return Math.Cos(Math.PI * j * (x - a) / (b - a)) * Math.PI * j / (b - a);
        }
        /// <summary>
        /// Возвращает значение второй производной базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение второй производной базисной функции.</returns>
        public double SecondDerivative(double x)
        {
            return -Math.Sin(Math.PI * j * (x - a) / (b - a)) * Math.Pow(Math.PI * j / (b - a), 2);
        }
        double a;
        double b;
        double ya;
        double yb;
        int j;
    }
}
