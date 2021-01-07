using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvanovAC.NM
{
    /// <summary>
    /// Задает функциональность класса, реализующего базисную функцию.
    /// </summary>
    public interface IBasisFunction
    {
        /// <summary>
        /// Возвращает значение базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение базисной функции.</returns>
        double Function(double x);
        /// <summary>
        /// Возвращает значение первой производной базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение первой производной базисной функции.</returns>
         double FirstDerivative(double x);
        /// <summary>
        /// Возвращает значение второй производной базисной функции.
        /// </summary>
        /// <param name="x">Значение аргумента.</param>
        /// <returns>Значение второй производной базисной функции.</returns>
        double SecondDerivative(double x);
    }
}
