using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace IvanovAC.Controls
{
    /// <summary>
    /// Элемент управления, предназначенный для отображения графиков.
    /// </summary>
    public partial class Plot : UserControl
    {

        Point centerPoint; // Положение начала координат в пикселях
        double scaleX; // Масштаб по оси X
        double scaleY; // Масштаб по оси Y
        int minPix; // размер "мертвой зоны" 
        Dictionary<string, PlotItem> items;
        Dictionary<string, CustListPointF> points;
        Dictionary<string, CustListPointF> pointsColor;
        Dictionary<string, List<CustListPointF>> pointsList;
        Size oldSize; // Старый размер области рисования графика
        Point oldMousePos; // Начальная точка движения


        /// <summary>
        /// Элемент управления, предназначеннный для отображения графиков.
        /// </summary>
        public Plot()
        {
            InitializeComponent();
            CenterPoint = new Point(pictureBoxPlot.Width / 2, pictureBoxPlot.Height / 2);
            this.items = new Dictionary<string, PlotItem>();
            this.points = new Dictionary<string, CustListPointF>();
            this.oldSize = this.pictureBoxPlot.Size;
            this.pointsList = new Dictionary<string, List<CustListPointF>>();
            SetLegendPanel();
            MinPix = 5;
            ScaleX = 1;
            ScaleY = 1;
            XName = "X";
            YName = "Y";
            XUnits = "in";
            YUnits = "in";
            labelXvalue.Text = "";
            labelYvalue.Text = "";

            this.pictureBoxPlot.MouseWheel += this.pictureBoxPlot_MouseWheel;
        }

        /// <summary>
        /// Рисует графики.
        /// </summary>
        public void Draw()
        {
            try
            {
                // Получаем пустое изображение с размерами элемента pictureBoxPlot,
                // выполняющее роль буфера
                this.pictureBoxPlot.Image = new Bitmap(this.pictureBoxPlot.Width,
                this.pictureBoxPlot.Height);                
                // Из изображения получаем объект Graphics,
                // предоставляющий методы рисования
                using (Graphics graphics = Graphics.FromImage(this.pictureBoxPlot.Image))   
                {
                    // Заливаем изображение цветом фона
                    graphics.Clear(this.pictureBoxPlot.BackColor);
                    // Создаем объект Pen, содержащий параметры рисования линий
                    using (Pen pen = new Pen(Color.Black))
                    {
                        // Устанавливаем толщину линий
                        pen.Width = 1f;
                        // Рисование осей
                        this.DrawAxes(graphics);
                        // Рисование графиков
                        if (this.items != null)
                        {
                            foreach (KeyValuePair<string, PlotItem> pair in this.items)
                            {
                                PlotItem item = pair.Value;
                                pen.Color = item.LineColor;
                                if (item.Function != null)
                                {
                                    this.DrawFunctionFromX(graphics, pen, item.Function);                                    
                                }
                            }
                        }

                        // Рисование графиков по точкам
                        if (this.points != null)
                        {
                            foreach (KeyValuePair<string, CustListPointF> pair in this.points)
                            {
                                CustListPointF item = pair.Value;
                                pen.Color = item.LineColor;
                                if (item.List != null)
                                {
                                    this.DrawPointsFromX(graphics, pen, item.List);
                                }
                            }
                        }
                        // Рисование графиков по листу массивов
                        if (this.pointsList != null)
                        {
                            foreach (KeyValuePair<string, List<CustListPointF>> pair in this.pointsList)
                            {
                                List <CustListPointF> items = pair.Value;
                                pen.Color = items[0].LineColor;
                                foreach(CustListPointF item in items)
                                {
                                    if (item.List != null)
                                    {
                                        this.DrawPointsFromX(graphics, pen, item.List);
                                    }
                                }
                            }
                        }
                    }
                }
                this.pictureBoxPlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка графика", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Рисует оси координат.
        /// </summary>
        /// <param name="Graphics">Холст.</param>
        /// <param name="Pen">Кисть.</param>
        private void DrawAxes(Graphics Graphics)
        {
            Pen p = new Pen(Color.Black, 1f);
             Graphics.DrawLine(p, 0, this.centerPoint.Y, this.pictureBoxPlot.Width, this.centerPoint.Y);
            //Graphics.DrawLine(Pen, this.centerPoint.X, 0, this.centerPoint.X, this.pictureBoxPlot.Height);
            

            // Задаем ориентировочное расстояние между линиями сетки в пикселях
            int delta = 80;
            // Задаем размер отступов,
            // необходимых для корректного для размещения подписей
            int xIndent = 30; // горизонтальный отступ
            int yIndent = 18; // вертикальный отступ            

            // Сетку рисовать будем серым цветом
            using (Pen pen = new Pen(Color.Gray))
            {
                // Задаем толщину линий стеки
                pen.Width = 0.7f;
                // Задаем стиль линий сетки - точками
                pen.DashStyle = DashStyle.Dot;
                // Задаем шрифт подписей
                Font font = new Font("Arial", 8);
                // Задаем цвет подписей
                Brush brush = Brushes.Black;


                #region Подписи к горизонтальной оси
                // Получаем ориентировочное расстояние между вертикальными линиями сетки в единицах оси X
                double Delta = delta / this.scaleX;
                // Приводим Delta к виду m * 10^k,
                // где 0.75 <= m && m <= 7.5
                int k = 0; // показатель степени                
                while (Delta > 7.5)
                {
                    Delta /= 10;
                    k++;
                }
                while (Delta < 0.75)
                {
                    Delta *= 10;
                    k--;
                }
                // Получаем фактическое расстояние между вертикальными линиями сетки в единицах оси X,
                // выбирая ближайшее значение из {1, 2, 5}
                double dX;
                if (3.5 < Delta) // && Delta <= 7.5
                {
                    dX = 5 * Math.Pow(10, k);
                }
                else
                {
                    if (1.5 < Delta) // && Delta <= 3.5
                    {
                        dX = 2 * Math.Pow(10, k);
                    }
                    else // 0.75 <= Delta && Delta <= 1.5
                    {
                        dX = 1 * Math.Pow(10, k);
                    }
                }
                // Вычисляем номер i самой левой вертикальной линии
                // i < 0 для X < 0, i > 0 для X > 0, i = 0 для X =0
                int i = (int)((xIndent - this.centerPoint.X) / (dX * this.scaleX));
                // Получаем координату левой линии в мировой системе
                double X = dX * i;
                // Получаем координату левой линии в системе монитора
                int x = this.centerPoint.X + (int)(X * this.scaleX);
                // Если линия оказалась в зоне левого отступа из-за погрешности округления,
                // то переходим к следующей
                if (x < xIndent)
                {
                    X = dX * ++i;
                    x = this.centerPoint.X + (int)(X * this.scaleX);
                }
                // Рисуем линии до зоны правого отступа
                while (x <= this.pictureBoxPlot.Width - xIndent)                   
{
                    // Рисуем вертикальную линию, оставляя снизу зону для подписи
                    Graphics.DrawLine(pen, x, 0, x, this.pictureBoxPlot.Height - yIndent);
                    // Формируем и выводим подпись
                    string mark = "";
                    if (k < 0)
                    {
                        // X - число дробное: выделяем место под дробную часть
                        mark = X.ToString("F" + (-k).ToString());
                    }
                    else
                    {
                        // X - число целое
                        mark = X.ToString();
                    }
                    // Получаем координаты левой верхней точки
                    // прямоугольника подписи и выводим ее
                    int xm = x - 7 - Math.Abs(k) * 3;
                    int ym = this.pictureBoxPlot.Height - 16;
                    Graphics.DrawString(mark, font, brush, xm, ym);
                    // Получаем параметры следующей линии
                    X = dX * ++i;
                    x = this.centerPoint.X + (int)(X * this.scaleX);
                }
                #endregion


                #region Подписи к вертикальной оси
                // Получаем ориентировочное расстояние между горизонтальными линиями сетки в единицах оси Y
                Delta = delta / this.scaleY;
                // Приводим Delta к виду m * 10^k,
                // где 0.75 <= m && m <= 7.5
                k = 0; // показатель степени                
                while (Delta > 7.5)
                {
                    Delta /= 10;
                    k++;
                }
                while (Delta < 0.75)
                {
                    Delta *= 10;
                    k--;
                }
                // Получаем фактическое расстояние между вертикальными линиями сетки в единицах оси Y,
                // выбирая ближайшее значение из {1, 2, 5}
                double dY;
                if (3.5 < Delta) // && Delta <= 7.5
                {
                    dY = 5 * Math.Pow(10, k);
                }
                else
                {
                    if (1.5 < Delta) // && Delta <= 3.5
                    {
                        dY = 2 * Math.Pow(10, k);
                    }
                    else // 0.75 <= Delta && Delta <= 1.5
                    {
                        dY = 1 * Math.Pow(10, k);
                    }
                }
                // Вычисляем номер i самой левой вертикальной линии
                // i < 0 для X < 0, i > 0 для X > 0, i = 0 для X =0
                i = (int)((yIndent - this.centerPoint.Y) / (dY * this.scaleY));
                // Получаем координату левой линии в мировой системе
                double Y = dY * i;
                // Получаем координату левой линии в системе монитора
                int y = this.centerPoint.Y + (int)(Y * this.scaleY);
                // Если линия оказалась в зоне левого отступа из-за погрешности округления,
                // то переходим к следующей
                if (y < yIndent)
                {
                    Y = dY * ++i;
                    y = this.centerPoint.Y + (int)(Y * this.scaleY);
                }
                // Рисуем линии до зоны правого отступа
                while (y <= this.pictureBoxPlot.Height - yIndent)
                {
                    // Рисуем вертикальную линию, оставляя снизу зону для подписи
                    Graphics.DrawLine(pen, 0, y, this.pictureBoxPlot.Width - xIndent, y);
                    // Формируем и выводим подпись
                    Y = -Y;
                    string mark = "";
                    if (k < 0)
                    {
                        // Y - число дробное: выделяем место под дробную часть
                        mark = Y.ToString("F" + (-k).ToString());
                    }
                    else
                    {
                        // Y - число целое
                        mark = Y.ToString();
                    }
                    // Получаем координаты левой верхней точки
                    // прямоугольника подписи и выводим ее
                    int xm = 0;
                    int ym = y - 7 - Math.Abs(k) * 3;
                    Graphics.DrawString(mark, font, brush, xm, ym);
                    // Получаем параметры следующей линии
                    Y = dY * ++i;
                    y = this.centerPoint.Y + (int)(Y * this.scaleY);
                }
                #endregion
            }

        }

        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {

        }

        /// <summary>
        /// Рисует график указанной функции y(x).
        /// </summary>
        /// <param name="Graphics">Холст.</param>
        /// <param name="Pen">Кисть.</param>
        /// <param name="Function">Функция, график которой нужно построить.</param>
        private void DrawFunctionFromX(Graphics Graphics, Pen Pen, Func<double, double> Function)
        {
            double X;
            double Y;
            int x = 0;
            X = (x - this.centerPoint.X) / this.scaleX;
            Y = Function(X);
            int y = this.centerPoint.Y - (int)(this.scaleY * Y);
            int xpr = x;
            int ypr = y;
            for (int i = 1; i < this.pictureBoxPlot.Width; i++)
            {
                x = i;
                X = (x - this.centerPoint.X) / this.scaleX;
                Y = Function(X);
                y = this.centerPoint.Y - (int)(this.scaleY * Y);
                Graphics.DrawLine(Pen, xpr, ypr, x, y);
                xpr = x;
                ypr = y;
            }
        }

        /// <summary>
        /// Рисует график по заданным точкам.
        /// </summary>
        /// <param name="Graphics">Холст.</param>
        /// <param name="Pen">Кисть.</param>
        /// <param name="points">Набор точек, по которым нужно построить график.</param>
        private void DrawPointsFromX(Graphics Graphics, Pen Pen, List<PointF> points)
        {
            PointF p1 = new PointF();
            PointF p2 = new PointF();           
            p1.X = (float)(this.centerPoint.X + (int)(this.scaleX * points[0].X));
            p1.Y = (float)(this.centerPoint.Y - (int)(this.scaleY * points[0].Y));
            //p1.Y = (float)((points[0].Y  - this.centerPoint.Y) / scaleY);

            for (int i = 1; i < points.Count; i++)
            {
                //p2.X = (float)((points[i].X  - this.centerPoint.X) / scaleX);
                //p2.Y = (float)((points[i].Y  - this.centerPoint.Y) / scaleY);
                p2.X = (float)(this.centerPoint.X + (int)(this.scaleX * points[i].X));
                p2.Y = (float)(this.centerPoint.Y - (int)(this.scaleY * points[i].Y));

                Graphics.DrawLine(Pen, p1, p2);
                //Graphics.DrawLine(Pen, new PointF() { X = (float)((this.centerPoint.X + 50) / scaleX), Y = this.centerPoint.Y }, new PointF() { X = 0, Y = 0 });

                p1.X = p2.X;
                p1.Y = p2.Y;
            }                      
        }

        /// <summary>
        /// Обновляет информацию о графике с указанным названием.
        /// Если графика с таким названием нет, то он будет создан.
        /// </summary>
        /// <param name="Legend">Название графика.</param>
        /// <param name="LineColor">Цвет графика.</param>
        /// <param name="Func">Функция y(x).</param>
        public void UpdateItem(string Legend, Color LineColor, Func<double, double> Func)
        {
            
            PlotItem item = new PlotItem(Legend, LineColor, Func);
            if (this.items.ContainsKey(Legend))
            {
                // Обновляем
                this.items[Legend] = item;
            }
            else
            {
                // Создаем новый
                this.items.Add(Legend, item);
            }
            SetLegendPanel();
        }

        /// <summary>
        /// Обновляет информацию о графике с указанным названием.
        /// Если графика с таким названием нет, то он будет создан.
        /// </summary>
        /// <param name="Legend">Название графика.</param>
        /// <param name="LineColor">Цвет графика.</param>
        /// <param name="Points">Набор точек, по которым нужно построить график.</param>
        public void UpdatePoints(string Legend, Color LineColor, List<PointF> Points)
        {
            
            CustListPointF item = new CustListPointF(Legend, LineColor, Points);
            if (this.points.ContainsKey(Legend))
            {
                // Обновляем
                this.points[Legend] = item;
            }
            else
            {
                // Создаем новый
                this.points.Add(Legend, item);
            }
            SetLegendPanel();
        }

        /// <summary>
        /// Обновляет информацию о графике с указанным названием.
        /// Если графика с таким названием нет, то он будет создан.
        /// </summary>
        /// <param name="Legend">Название графика.</param>
        /// <param name="LineColor">Цвет графика.</param>
        /// <param name="Points">Набор точек, по которым нужно построить график.</param>
        public void AddPoints(string Legend, Color LineColor, List<PointF> Points)
        {
            CustListPointF item = new CustListPointF(Legend, LineColor, Points);                                      
            if (this.pointsList.ContainsKey(Legend))
            {
                // Обновляем
                this.pointsList[Legend].Add(item);
            }
            else
            {
                // Создаем новый
                List<CustListPointF> list = new List<CustListPointF>();
                list.Add(item);
                this.pointsList.Add(Legend, list);
                this.points.Add(Legend, item);
            }
            SetLegendPanel();
        }

        /// <summary>
        /// Удаляет все элементы из коллекции
        /// </summary>    
        public void Clear()
        {
            items.Clear();
        }


        /// <summary>
        /// Задает новые параметры начала координат и масштаба,
        /// перерисовывая график.
        /// </summary>
        /// <param name="Center">Новый центр взгляда в координатах монитора.
        /// </param>
        /// <param name="ScaleX">Новый масштаб по оси X.</param>
        /// <param name="ScaleY">Новый масштаб по оси Y.</param>
        private void SetCamera(Point Center, double ScaleX, double ScaleY)
        {
            // Пересчитываем положение начала координат
            this.centerPoint.X = (int)Math.Round(Center.X + ScaleX / this.scaleX *
            (this.centerPoint.X - Center.X), MidpointRounding.AwayFromZero);
            this.centerPoint.Y = (int)Math.Round(Center.Y + ScaleY / this.scaleY *
            (this.centerPoint.Y - Center.Y), MidpointRounding.AwayFromZero);
            // Сохраняем новые значения масштаба
            this.scaleX = ScaleX;
            this.scaleY = ScaleY;
            // Обновляем содержимое буфера
            this.Draw();
        }

        /// <summary>
        /// Обновляет состояние панели "Легенда".
        /// </summary>
        private void SetLegendPanel()
        {
            // Очищаем таблицу
            this.dataGridViewLegend.Rows.Clear();            
            // Применяем настройки видимости
            if (this.items.Count == 0 && this.points.Count == 0)
            {
                // Если графиков нет, то скрываем
                this.panelLegend.Visible = false;
                this.buttonLegend.Visible = false;
                this.buttonLegend.Text = @"/\";
            }
            else
            {
                // Если графики есть, то отображаем
                this.panelLegend.Visible = true;
                this.buttonLegend.Visible = true;
                this.buttonLegend.Text = @"\/";
            }
            // Запрещаем пользователю менять размеры ячеек
            this.dataGridViewLegend.AllowUserToResizeColumns = false;
            this.dataGridViewLegend.AllowUserToResizeRows = false;
            // Устанавливаем размеры панели и таблицы
            // в зависимости от количества графиков
            int rowHeight = 20; // высота ячейки
            this.dataGridViewLegend.Height =
            (rowHeight + 2) * (this.items.Count + this.points.Count) + 3;
            this.panelLegend.Height = this.dataGridViewLegend.Height + 22;
            this.panelLegend.Location =
            new Point(this.pictureBoxPlot.Width - this.panelLegend.Width,
            this.pictureBoxPlot.Height - this.panelLegend.Height - 25);
            // Заполняем таблицу новыми данными
            this.dataGridViewLegend.RowCount = this.items.Count + this.points.Count;
            int i = 0;            
            foreach (KeyValuePair<string, PlotItem> pair in this.items)
            {
                // Выводим название графика в ячейку "Легенда"
                this.dataGridViewLegend["ChartLegend", i].Value = pair.Value.Legend;
                // Закрашиваем цветом графика ячейку "Цвет"                
                this.dataGridViewLegend["ChartColor", i].Style.BackColor = pair.Value.LineColor;
                i++;
            }

            foreach (KeyValuePair<string, CustListPointF> pair in this.points)
            {
                // Выводим название графика в ячейку "Легенда"
                this.dataGridViewLegend["ChartLegend", i].Value = pair.Value.Legend;
                // Закрашиваем цветом графика ячейку "Цвет"                
                this.dataGridViewLegend["ChartColor", i].Style.BackColor = pair.Value.LineColor;
                i++;
            }

            /*foreach (KeyValuePair<string, List<CustListPointF>> pair in this.pointsList)
            {
                CustListPointF item = pair.Value[0];
                // Выводим название графика в ячейку "Легенда"
                this.dataGridViewLegend["ChartLegend", i].Value = item.Legend;
                // Закрашиваем цветом графика ячейку "Цвет"                
                this.dataGridViewLegend["ChartColor", i].Style.BackColor = item.LineColor;
                i++;
            }*/

        }

        /// <summary>
        /// Возвращает ширину pictureBoxPlot
        /// </summary>
        [Category("Plot")]
        public int WidthPictureBox
        {
            get
            {
                return pictureBoxPlot.Width;
            }
            set
            {
                pictureBoxPlot.Width = value;
            }
        }

        /// <summary>
        /// Возвращает высоту pictureBoxPlot
        /// </summary>
        [Category("Plot")]
        public int HeightPictureBox
        {
            get
            {
                return pictureBoxPlot.Height;
            }
            set
            {
                pictureBoxPlot.Height = value;
            }
        }

        /// <summary>
        /// Возвращает либо задает центр мировых координат на плоскости контрола.
        /// </summary>
        [Category("Plot")]
        public Point CenterPoint
        {
            get
            {
                return this.centerPoint;
            }
            set
            {
                this.centerPoint = value;
            }
        }

        /// <summary>
        /// Возвращает либо задает длину стороны квадрата в пикселях,
        /// перемещение курсора внутри которого будет считаться точечным действием.
        /// </summary>
        [Category("Plot")]
        public int MinPix
        {
            get
            {
                return this.minPix;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Значение MinPix должно быть > 0");
                }
                this.minPix = value;
            }
        }

        /// <summary>
        /// Возвращает либо задает коэффициент масштабирования по оси X
        /// (задает число пикселей, соответствующее единице измерения вдоль оси).
        /// </summary>
        [Category("Plot")]
        public double ScaleX
        {
            get
            {
                return this.scaleX;
            }
            set
            {
                this.scaleX = value;
            }
        }

        /// <summary>
        /// Возвращает либо задает коэффициент масштабирования по оси Y
        /// (задает число пикселей, соответствующее единице измерения вдоль оси).
        /// </summary>
        [Category("Plot")]
        public double ScaleY
        {
            get
            {
                return this.scaleY;
            }
            set
            {
                this.scaleY = value;
            }
        }

        /// <summary>
        /// Возвращает либо задает название величины, откладываемой по оси X.
        /// </summary>
        [Category("Plot")]
        public string XName
        {
            get
            {
                return this.labelXname.Text.TrimEnd(' ', '=');
            }
            set
            {
                this.labelXname.Text = value + " =";
            }
        }

        /// <summary>
        /// Возвращает либо задает название единицы измерения по оси X.
        /// </summary>
        [Category("Plot")]
        public string XUnits
        {
            get
            {
                return this.labelXunits.Text.Trim('[', ']');
            }
            set
            {
                if (value != "")
                {
                    this.labelXunits.Text = "[" + value + "]";
                }
                else
                {
                    this.labelXunits.Text = "";
                }
            }
        }

        /// <summary>
        /// Возвращает либо задает название величины, откладываемой по оси Y.
        /// </summary>
        [Category("Plot")]
        public string YName
        {
            get
            {
                return this.labelYname.Text.TrimEnd(' ', '=');
            }
            set
            {
                this.labelYname.Text = value + " =";
            }
        }

        /// <summary>
        /// Возвращает либо задает название единицы измерения по оси Y.
        /// </summary>
        [Category("Plot")]
        public string YUnits
        {
            get
            {
                return this.labelYunits.Text.Trim('[', ']');
            }
            set
            {
                if (value != "")
                {
                    this.labelYunits.Text = "[" + value + "]";
                }
                else
                {
                    this.labelYunits.Text = "";
                }
            }
        }

        private void pictureBoxPlot_Click(object sender, EventArgs e)
        {

        }

        private void panelInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxPlot_SizeChanged(object sender, EventArgs e)
        {
            // Получаем коэффициенты растяжения (сжатия) по осям
            float kx = (float)this.pictureBoxPlot.Size.Width /
            (float)this.oldSize.Width;
            float ky = (float)this.pictureBoxPlot.Size.Height /
            (float)this.oldSize.Height;
            // Пересчитываем положение начала координат и масштаб
            this.centerPoint.X = (int)Math.Round(kx * this.centerPoint.X,
            MidpointRounding.AwayFromZero);
            this.centerPoint.Y = (int)Math.Round(ky * this.centerPoint.Y,
            MidpointRounding.AwayFromZero);
            this.scaleX *= kx;
            this.scaleY *= ky;
            // Сохраняем размеры области рисования
            this.oldSize = this.pictureBoxPlot.Size;
            // Обновляем содержимое буфера
            this.Draw();
        }

        private void pictureBoxPlot_MouseEnter(object sender, EventArgs e)
        {
            // Передача фокуса для корректной работы события MouseWheel
            this.pictureBoxPlot.Focus();                   
        }

        private void pictureBoxPlot_MouseLeave(object sender, EventArgs e)
        {
            labelXvalue.Text = "";
            labelYvalue.Text = "";
        }

        private void pictureBoxPlot_MouseMove(object sender, MouseEventArgs e)
        {           

            if (e.Button == MouseButtons.Left)
            {
                // Пересчитываем положение начала координат                
                int a = oldMousePos.X - e.Location.X;
                int b = oldMousePos.Y - e.Location.Y;               
                this.centerPoint.X -= a;               
                this.centerPoint.Y -= b;                
                // Обновляем содержимое буфера
                //p1.X = (float)(this.centerPoint.X + (int)(this.scaleX * points[0].X));
                // p1.Y = (float)(this.centerPoint.Y - (int)(this.scaleY * points[0].Y));
                this.Draw();
            }

            #region Рисование рамки для функции приближения
            if (e.Button == MouseButtons.Middle)
            {
                // Получаем размеры прямоугольника рамки              
                int width = Math.Abs(e.Location.X - this.oldMousePos.X);
                int height = Math.Abs(e.Location.Y - this.oldMousePos.Y);
                // Проверяем, была ли преодолена "мертвая" зона хотя бы в одном
                // направлении
                if (width >= this.minPix || height >= this.minPix)
                {
                    // Определяем левую верхнюю вершину прямоугольника рамки
                    int x = Math.Min(e.Location.X, this.oldMousePos.X);
                    int y = Math.Min(e.Location.Y, this.oldMousePos.Y);
                    // Рисуем рамку на форме, не затрагивая содержимое буфера
                    using (Graphics g = this.pictureBoxPlot.CreateGraphics())
                    {
                        using (Pen pen = new Pen(Color.Black))
                        {
                            pen.DashStyle = DashStyle.Dot;
                            g.DrawRectangle(pen, x, y, width, height);
                        }
                    }
                }                
            }
            #endregion           
            // Стираем старую рамку
            this.pictureBoxPlot.Refresh();
            
        }

        private void pictureBoxPlot_MouseUp(object sender, MouseEventArgs e)
        {          

            #region Изменение масштаба: приближение по рамке либо клику
            if (e.Button == MouseButtons.Middle)
            {
                // Получаем размеры выделенной области
                int width = Math.Abs(e.Location.X - this.oldMousePos.X);
                int height = Math.Abs(e.Location.Y - this.oldMousePos.Y);
                if (width < this.minPix || height < this.minPix)
                {
                    if (width < this.minPix && height < this.minPix)
                    {
                        // Точечное действие - приближение по клику
                        this.SetCamera(e.Location, 2f * this.scaleX, 2f * this.scaleY);
                    }
                    else
                    {
                        // Ничего не делаем и стираем рамку
                        this.pictureBoxPlot.Refresh();
                    }
                }
                else
                {
                    // Приближение по рамке:
                    // Получаем коэффициенты изменения масштаба по осям
                    double kx = (double)this.pictureBoxPlot.Width / (double)width;
                    double ky = (double)this.pictureBoxPlot.Height / (double)height;

                    // Вычисляем координаты центра выделенной области
                    int x = (int)Math.Round(0.5d * (e.Location.X + this.oldMousePos.X),
                    MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(0.5d * (e.Location.Y + this.oldMousePos.Y),
                    MidpointRounding.AwayFromZero);
                    Point center = new Point(x, y);
                    // Устанавливаем камеру в центр выделенной области
                    this.SetCamera(center, kx * this.scaleX, ky * this.scaleY);
                }
            }
            #endregion

            if (e.Button == MouseButtons.Right)
            {
                // Получаем размеры выделенной области
                int width = Math.Abs(e.Location.X - this.oldMousePos.X);
                int height = Math.Abs(e.Location.Y - this.oldMousePos.Y);
                if (width < this.minPix || height < this.minPix)
                {
                    if (width < this.minPix && height < this.minPix)
                    {
                        // Точечное действие - отдаление по клику
                        this.SetCamera(e.Location, 0.5f * this.scaleX, 0.5f * this.scaleY);
                    }
                    else
                    {
                        // Ничего не делаем и стираем рамку
                        this.pictureBoxPlot.Refresh();
                    }
                }
            }
        }

        private void pictureBoxPlot_MouseDown(object sender, MouseEventArgs e)
        { 
            oldMousePos = e.Location;

            double X;
            double Y;
            X = (oldMousePos.X - this.centerPoint.X) / this.scaleX;
            Y = (oldMousePos.Y - this.centerPoint.Y) / this.scaleY;
            labelXvalue.Text = X.ToString("F8");
            labelYvalue.Text = Y.ToString("F8");
        }

        private void pictureBoxPlot_MouseWheel(object sender, MouseEventArgs e)
        {
            // Получаем коэффициент изменения масштаба
            double k = Math.Pow(2d, e.Delta / SystemInformation.MouseWheelScrollDelta);
            // Устанавливаем камеру в точку расположения курсора мыши
            this.SetCamera(e.Location, k * this.scaleX, k * this.scaleY);
        }

        private void pictureBoxPlot_MouseHover(object sender, EventArgs e)
        {            
        }

        private void buttonLegend_Click(object sender, EventArgs e)
        {
            if (this.panelLegend.Visible)
            {
                this.panelLegend.Visible = false;
                this.buttonLegend.Text = @"/\";
            }
            else
            {
                this.panelLegend.Visible = true;
                this.buttonLegend.Text = @"\/";
            }
        }

        private void dataGridViewLegend_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {            
        }

        private void labelXunits_Click(object sender, EventArgs e)
        {

        }
    }
}
