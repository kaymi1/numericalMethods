using System.Collections.Generic;
using System.Drawing;

namespace IvanovAC.Controls
{
    class CustListPointF
    {
        string legend;
        Color lineColor;
        List<PointF> list;


        /// <summary>
        /// Содержит информацию о т функции.
        /// </summary>
        /// <param name="Legend">Название графика.</param>
        /// <param name="LineColor">Цвет графика.</param>
        /// <param name="List">Список точек.</param>
        internal CustListPointF(string Legend, Color LineColor, List<PointF> List)
        {
            this.legend = Legend;
            this.lineColor = LineColor;
            this.list = List;
        }



        /// <summary>
        /// Возвращает название графика.
        /// </summary>
        internal string Legend
        {
            get
            {
                return this.legend;
            }

        }

        /// <summary>
        /// Возвращает либо задает цвет графика.
        /// </summary>
        internal Color LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
            }
        }

        /// <summary>
        /// Возвращает либо задает список точек.
        /// </summary>
        internal List<PointF> List
        {
            get
            {
                return this.list;
            }
            set
            {
                this.list = value;
            }
        }               
    }
}
