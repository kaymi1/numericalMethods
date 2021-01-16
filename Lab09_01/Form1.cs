using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IvanovAC.NM;

namespace Lab09_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.plot1.CenterPoint = new Point(this.plot1.WidthPictureBox / 2, this.plot1.HeightPictureBox / 2);
            /* Масштаб для основной задачи 
            this.plot1.ScaleX = 250000;
            this.plot1.ScaleY = 150000;*/

            /* Масштаб для тестовой задачи */
            this.plot1.ScaleX = 700000;
            this.plot1.ScaleY = 110000;
        }

        /* < Начальные данные >  */ 
        private int m = 1;
        private int n = 3;

        private int a1 = 1;
        private int a2 = 4;
        private int a3 = 15;

        private double ya = -0.5;
        private double yb = -0.8;

        private double A = 0;
        private double B = 0.7;
        /* </ Начальные данные >  */

        private double Ftest(double X, double Y)
        {
            return 0;
        }
        private double Ptest(double X, double Y)
        {
            return m;
        }
        private double Qtest(double X, double Y)
        {
            return n;
        }


        private double Fmain(double X, double Y)
        {
            return 0;
        }
        private double Pmain(double X, double Y)
        {
            return (a1 + a2 * X) / (X * X + 1);
        }
        private double Qmain(double X, double Y)
        {
            return (a3) / (Math.Sqrt(1 - X * X));
        }


        private void Button_Click(object sender, EventArgs e)
        {
            int.TryParse(count.Text, out int items);


            if ((string)this.choiceProblemBox.SelectedItem == "Тестовая задача")
            {
                List<PointF> points = BoundaryValueProblem1D.FiniteDiffMethod(A, B, ya, yb, this.Ptest, this.Qtest, this.Ftest, items);
                this.plot1.UpdatePoints("Разностный метод", Color.Black, points);
                this.plot1.Draw();

                points = CauchyProblem.AnalyticSolveDiff2(A, B, ya, items);
                this.plot1.UpdatePoints("Аналит. решение", Color.Green, points);
                this.plot1.Draw();
            }
            else if ((string)this.choiceProblemBox.SelectedItem == "Основная задача")
            {
                List<PointF> points = BoundaryValueProblem1D.FiniteDiffMethod(A, B, ya, yb, this.Pmain, this.Qmain, this.Fmain, items);
                this.plot1.UpdatePoints("Разностный метод", Color.Black, points);
                this.plot1.Draw();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void plot1_Load(object sender, EventArgs e)
        {

        }
    }
}
