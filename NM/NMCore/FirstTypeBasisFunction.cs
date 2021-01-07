namespace IvanovAC.NM
{
    /// <summary>
    /// Базисная функция для граничного условия первого рода.
    /// </summary>
    public class FirstTypeBasisFunction : IBasisFunction
    {
        /// <summary>
        /// Базисная функция для граничного условия первого рода.
        /// </summary>
        /// <param name="a">Левый конец отрезка.</param>
        /// <param name="b">Правый конец отрезка.</param>
        /// <param name="ya">Значение функции в точке a.</param>
        /// <param name="yb">Значение функции в точке b.</param>
        public FirstTypeBasisFunction(double a, double b, double ya,
        double yb)
        {
            this.a = a;
            this.b = b;
            this.ya = ya;
            this.yb = yb;
        }
        /// <summary>
        /// Возвращает значение базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение базисной функции.</returns>
        public double Function(double x)
        {
            return this.ya + (this.yb - this.ya) * (x - this.a) / (this.b - this.a);
        }
         /// <summary>
         /// Возвращает значение первой производной базисной функции.
         /// </summary>
         /// <param name="x">Значение аргумента.</param>
         /// <returns>Значение первой производной базисной функции.</returns>
         public double FirstDerivative(double x)
         {
            return (this.yb - this.ya) / (this.b - this.a);
         }
         /// <summary>
         /// Возвращает значение второй производной базисной функции.
         /// </summary>
         /// <param name="x">Значение аргумента.</param>
         /// <returns>Значение второй производной базисной функции.</returns>
         public double SecondDerivative(double x)
         {
            return 0;
         }
         double a;
         double b;
         double ya;
         double yb;
    }
}
