using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IvanovAC.NM;

namespace Lab05_01
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
            this.plot1.CenterPoint = new Point(this.plot1.WidthPictureBox / 2, this.plot1.HeightPictureBox / 2);
            //this.plot1.ScaleX = 90000;
            //this.plot1.ScaleY = 10000;
            //this.plot1.ScaleX = 16000;
            //this.plot1.ScaleY = 7500;
            this.plot1.ScaleX = 170000;
            this.plot1.ScaleY = 90000;
            //this.plot1.UpdateItem("sin(x)", Color.Green, Math.Sin);

        }

        private double F(double X, double Y)
        {
            return Math.Sin(X) - Y / X;
        }
        private double F1(double X, double Y)
        {
            return (-12 / Math.Pow(X, 3)) + Y / X;
        }
        private double F2(double X, double Y)
        {
            return 1 / (2 * Math.Exp(X) + 1);
        }
        private double F3(double X, double Y)
        {
            return 2 * X * (X - 1);
        }
        private double T(double X, double Y)
        {
            return -Math.Cos(X) + Math.Sin(X)/X + (1 - Math.PI)/X;
        }
        private double A(double X, double Y)
        {
            return 4/Math.Pow(X,2);
        }

        private void plot1_Load(object sender, EventArgs e)
        {

        }
        private double Q(double X, double Y)
        {
            return 2;
        }
        private double P(double X, double Y)
        {
            return 0;
        }


        private void Button_Click(object sender, EventArgs e)
        {
            int.TryParse(count.Text, out int n);
            double.TryParse(texte.Text, out double E);
            if ((string)this.comboBox1.SelectedItem == "Разностный мет. и аналит. реш.")
            {
                List<PointF> points = BoundaryValueProblem1D.FiniteDiffMethod(0, 2, Math.Log(27), (Math.Log(2 * Math.Exp(2) + 1) - 2) * (1 + 2 * Math.Exp(2)), this.P, this.Q, this.F2, n);
                this.plot1.UpdatePoints("Разностный метод", Color.Black, points);
                this.plot1.Draw();

                points = CauchyProblem.AnalyticSolveDiff2(0, 2, Math.Log(27), n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Green, points);
                this.plot1.Draw();

            }
            if ((string)this.comboBox1.SelectedItem == "Разностный метод")
            {
                List<PointF> points = BoundaryValueProblem1D.FiniteDiffMethod(0, 1, 1, 1, this.P, this.Q, this.F3, n);
                this.plot1.UpdatePoints("Разностный метод", Color.Black, points);
                this.plot1.Draw();                
            }
            /*if ((string)this.comboBox1.SelectedItem == "Разностный метод")
            {
                List<PointF> points = BoundaryValueProblem1D.FiniteDiffMethod(0, 2, Math.Log(27), (Math.Log(2 * Math.Exp(2) + 1) - 2) * (1 + 2 * Math.Exp(2)), this.P, this.Q, this.F2, n);
                this.plot1.UpdatePoints("Разностный метод", Color.Black, points);
                this.plot1.Draw();
            }*/

            /*if ((string)this.comboBox1.SelectedItem == "Аналитическое решение")
            {
                List<PointF> points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);
                this.plot1.Draw();
            }
            if ((string)this.comboBox1.SelectedItem == "Простейший метод Эйлера")
            {
                List<PointF> points = CauchyProblem.SimpleEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Прост. мет. Эйлера", Color.Black, points);
                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Исправленный метод Эйлера")
            {
                List<PointF> points = CauchyProblem.CorrectedEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Испр. мет. Эйлера", Color.Black, points);
                this.plot1.Draw();              
            }        

            else if ((string)this.comboBox1.SelectedItem == "Модифицированный метод Эйлера")
            {
                List<PointF> points = CauchyProblem.ModifiedEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Мод. мет. Эйлера", Color.Green, points);
                this.plot1.Draw();

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);
                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Сравнение методов Эйлера и аналит. реш.")
            {
                List<PointF> points = CauchyProblem.SimpleEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Прост. мет. Эйлера", Color.Black, points);
                this.plot1.Draw();

                points = CauchyProblem.CorrectedEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Испр. мет. Эйлера", Color.Red, points);
                this.plot1.Draw();

                points = CauchyProblem.ModifiedEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Мод. мет. Эйлера", Color.Green, points);
                this.plot1.Draw();

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Blue, points);
                this.plot1.Draw();
            }

             else if ((string)this.comboBox1.SelectedItem == "Мет. Рунге-Кутты 3 пр. точ. и аналит. реш.")
             {
                 List<PointF> points = CauchyProblem.Simpson(1, 6, 4, this.F1, n);
                 this.plot1.UpdatePoints("Метод РК 3 пр. точ.", Color.Green, points);               
                 this.plot1.Draw();

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Blue, points);
                this.plot1.Draw();
            }

             else if ((string)this.comboBox1.SelectedItem == "Мет. Рунге-Кутты 4 пр. точ.")
             {
                 List<PointF> points = CauchyProblem.RungeKutta4(1, 6, 4, this.F1, n);
                 this.plot1.UpdatePoints("Метод РК 4 пр. точ.", Color.Black, points);                 
             }

            else if ((string)this.comboBox1.SelectedItem == "Мет. Рунге-Кутты 4 пр. точ. и аналит. реш.")
            {
                List<PointF> points = CauchyProblem.RungeKutta4(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Метод РК 4 пр. точ.", Color.Black, points);

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Blue, points);
                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Мет. Рунге-Кутты 3 и 4 пр. т. и ан. реш.")
            {
                List<PointF> points = CauchyProblem.Simpson(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Метод РК 3 пр. точ.", Color.Black, points);

                points = CauchyProblem.RungeKutta4(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Метод РК 4 пр. точ.", Color.Red, points);

                points = CauchyProblem.SimpleEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Прост. мет. Эйлера", Color.DarkSlateGray, points);

                points = CauchyProblem.CorrectedEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Испр. мет. Эйлера", Color.Brown, points);

                points = CauchyProblem.ModifiedEuler(1, 6, 4, this.F1, n);
                this.plot1.UpdatePoints("Модиф. мет. Эйлера", Color.Yellow, points);

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.DarkGreen, points);

                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Все методы и аналит. решение")
            {
                List<PointF> points = CauchyProblem.SimpleEuler(Math.PI, 3 * Math.PI, 1 / Math.PI, this.F, n);
                this.plot1.UpdatePoints("Прост. метод Эйлера", Color.Blue, points);

                points = CauchyProblem.CorrectedEuler(Math.PI, 3 * Math.PI, 1 / Math.PI, this.F, n);
                this.plot1.UpdatePoints("Испр. метод Эйлера", Color.Black, points);

                points = CauchyProblem.ModifiedEuler(Math.PI, 3 * Math.PI, 1 / Math.PI, this.F, n);
                this.plot1.UpdatePoints("Модиф. метод Эйлера", Color.Red, points);

                points = CauchyProblem.AnalyticSolve(Math.PI, 3 * Math.PI, 1 / Math.PI, this.T, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.DarkGreen, points);                

                this.plot1.Draw();
            }

             */
            if ((string)this.comboBox1.SelectedItem == "Мет. Эйлера с итерациями и аналит. решение")
            {
                List<PointF> points = CauchyProblem.IterativeEuler(1, 6, 4, this.F1, n, E);
                this.plot1.UpdatePoints("Мет. Эйлера с итер.", Color.Green, points);

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);

                this.plot1.Draw();
            }

           else if ((string)this.comboBox1.SelectedItem == "Мет. Эйлера с итерациями")
            {
                List<PointF> points = CauchyProblem.IterativeEuler(1, 6, 4, this.F1, n, E);
                this.plot1.UpdatePoints("Мет. Эйлера с итер.", Color.Green, points);                
                this.plot1.Draw();
            }

           /* else if ((string)this.comboBox1.SelectedItem == "Мет. двухшаг. прогноза и корр. и аналит. реш.")
            {
                List<PointF> points = CauchyProblem.PredictorCorrector2(1, 6, 4, this.F1, n, E);
                this.plot1.UpdatePoints("Мет. двухш. прогн. и корр.", Color.Green, points);

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);

                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Мет. двухшаг. прогноза и корр.")
            {
                List<PointF> points = CauchyProblem.PredictorCorrector2(1, 6, 4, this.F1, n, E);
                this.plot1.UpdatePoints("Мет. двухш. прогн. и корр.", Color.Black, points);                

                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Мет. Милна и аналит. решение")
            {
                List<PointF> points = CauchyProblem.Milne(1, 6, 4, this.F1, n, E);                
                this.plot1.UpdatePoints("Метод Милна", Color.Green, points);

                points = CauchyProblem.AnalyticSolve(1, 6, 4, this.A, n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);

                this.plot1.Draw();
            }

            else if ((string)this.comboBox1.SelectedItem == "Метод Милна")
            {
                List<PointF> points = CauchyProblem.Milne(1, 6, 4, this.F1, n, E);
                this.plot1.UpdatePoints("Метод Милна", Color.Black, points);          

                this.plot1.Draw();
            }*/
            if ((string)this.comboBox1.SelectedItem == "Аналитическое решение")
            {
                List<PointF> points = CauchyProblem.AnalyticSolveDiff2(0, 2, Math.Log(27), n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);
                this.plot1.Draw();
            }
            else if ((string)this.comboBox1.SelectedItem == "Разн. мет. 1-го пор. аппр.")
            {
                List<PointF> points = CauchyProblem.FiniteDiffMethod1(0, 2, Math.Log(27), Math.Log(9) - 1, this.F2, n);
                this.plot1.UpdatePoints("Разн. мет. 1 пор. аппр.", Color.Black, points);
                this.plot1.Draw();                
            }
            else if ((string)this.comboBox1.SelectedItem == "Разн. мет. 2-го пор. аппр.")
            {
                List<PointF> points = CauchyProblem.FiniteDiffMethod2(0, 2, Math.Log(27), Math.Log(9) - 1, this.F2, n);
                this.plot1.UpdatePoints("Разн. мет. 2 пор. аппр.", Color.Blue, points);
                this.plot1.Draw();                
            }
            else if ((string)this.comboBox1.SelectedItem == "Разн. мет. 1-го пор. аппр. и аналит. реш.")
            {
                List<PointF> points = CauchyProblem.FiniteDiffMethod1(0, 2, Math.Log(27), Math.Log(9) - 1, this.F2, n);
                this.plot1.UpdatePoints("Разн. мет. 1 пор. аппр.", Color.Violet, points);
                this.plot1.Draw();

                points = CauchyProblem.AnalyticSolveDiff2(0, 2, Math.Log(27), n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);
                this.plot1.Draw();
            }
            else if ((string)this.comboBox1.SelectedItem == "Разн. мет. 2-го пор. аппр. и аналит. реш.")
            {
                List<PointF> points = CauchyProblem.FiniteDiffMethod2(0, 2, Math.Log(27), Math.Log(9) - 1, this.F2, n);
                this.plot1.UpdatePoints("Разн. мет. 2 пор. аппр.", Color.Green, points);
                this.plot1.Draw();

                points = CauchyProblem.AnalyticSolveDiff2(0, 2, Math.Log(27), n);
                this.plot1.UpdatePoints("Аналит. решение", Color.Black, points);
                this.plot1.Draw();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
