﻿using System;
using System.Collections.Generic;
using System.Drawing;


namespace IvanovAC.Controls
{
    /// <summary>
    /// Содержит информацию о графике функции.
    /// </summary>
    internal class PlotItem
    {

        string legend;
        Color lineColor;
        Func<double, double> func;
        

        /// <summary>
        /// Содержит информацию о графике функции.
        /// </summary>
        /// <param name="Legend">Название графика.</param>
        /// <param name="LineColor">Цвет графика.</param>
        /// <param name="Func">Функция y(x).</param>
        internal PlotItem(string Legend, Color LineColor, Func<double, double> Func)
        {
            this.legend = Legend;
            this.lineColor = LineColor;
            this.func = Func;            
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
        /// Возвращает либо задает функцию y(x).
        /// </summary>
        internal Func<double, double> Function
        {
            get
            {
                return this.func;
            }
            set
            {
                this.func = value;
            }
        }
        
    }
}
