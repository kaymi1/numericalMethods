using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using IvanovAC.NM;

namespace problem2
{
    public partial class Form1 : Form
    {
        enum ColorGraphic : byte
        {
            Violet = 0,
            Green,
            YellowGreen, 
            Blue,
            Yellow, 
            Brown,
            Beige,
            Aqua,
            Azure,
            Olive
        }

        public Form1()
        {
            InitializeComponent();
            this.plot1.CenterPoint = new Point(this.plot1.WidthPictureBox / 2, this.plot1.HeightPictureBox / 2);
            /* Тестовая задача
            this.plot1.ScaleX = 2500000;
            this.plot1.ScaleY = 150000; */

            /* Основная задача */
            this.plot1.ScaleX = 10000000;
            this.plot1.ScaleY = 110000;               
        }

        /* </ Начальные данные >  */
        private int m = 1;
        private int n = 3;

        private int u1 = 20;
        private int u2 = 70;

        private int mu1 = 1;
        private int mu2 = 0;
        private int mu3 = 0;

        private int v1 = 0;
        private int v2 = 1;
        private int v3 = 20;

        private double l1 = 0.1;

        private double A = 0;
        private double B = 1;
        /* </ Начальные данные >  */ 

        private void Button_Click(object sender, EventArgs e)
        {
            int.TryParse(count.Text, out int items);

           
            if ((string)this.choiceProblemBox.SelectedItem == "Тестовая задача")
            {

                List<PointF[]> pointsList = BoundaryValueProblem1D.AnalyticSolveHeatEquation(A, B, items); 
                foreach(PointF[] pointsItem in pointsList)
                {
                    List<PointF> points = new List<PointF>(pointsItem);
                    this.plot1.AddPoints("Аналитическое решение", Color.Black, points);
                    this.plot1.Draw();

                }

                pointsList = BoundaryValueProblem1D.FiniteDiffMethodForHeatEquation(A, B, items, u1, u2, l1); 
                foreach (PointF[] pointsItem in pointsList)
                {
                    List<PointF> points = new List<PointF>(pointsItem);
                    this.plot1.AddPoints("Разностный метод", Color.Red, points);
                    this.plot1.Draw();
                }
            }
            else if ((string)this.choiceProblemBox.SelectedItem == "Тестовая задача (дискр. м. времени)")
            {                
                ColorGraphic color = new ColorGraphic();
                List<PointF[]> pointsList = BoundaryValueProblem1D.AnalyticSolveHeatEquation(A, B, items);
                List<PointF> points = new List<PointF>(pointsList[0]);
                this.plot1.UpdatePoints("t = 0 аналит. реш.", Color.Red, points);
                this.plot1.Draw();
                for (int j = 1; j < 100; j*=5)
                {
                    points = new List<PointF>(pointsList[j]);
                    this.plot1.UpdatePoints("t = dt*" + j + " ан. реш.", Color.FromName(color.ToString()), points);
                    color++;
                    this.plot1.Draw();
                }
                points = new List<PointF>(pointsList[pointsList.Count - 1]);
                this.plot1.UpdatePoints("t = tend аналит. реш.", Color.Black, points);
                this.plot1.Draw();
                        
                
                pointsList = BoundaryValueProblem1D.FiniteDiffMethodForHeatEquation(A, B, items, u1, u2, l1);
                points = new List<PointF>(pointsList[0]);
                this.plot1.UpdatePoints("t = 0 числ. реш.", Color.Tan, points);
                this.plot1.Draw();
                for (int j = 1; j < 100; j *= 5)
                {
                    points = new List<PointF>(pointsList[j]);
                    this.plot1.UpdatePoints("t = dt*" + j + " чис. реш.", Color.FromName(color.ToString()), points);
                    color++;
                    this.plot1.Draw();
                }
                points = new List<PointF>(pointsList[pointsList.Count - 1]);
                this.plot1.UpdatePoints("t = tend числ. реш.", Color.Purple, points);
                this.plot1.Draw();
            }
            else if ((string)this.choiceProblemBox.SelectedItem == "Основная задача")
            {
                List<PointF[]> pointsList = BoundaryValueProblem1D.FiniteDiffMethodForHeatEquation(A, B, items, u1, u2, l1);
                foreach (PointF[] pointsItem in pointsList)
                {
                    List<PointF> points = new List<PointF>(pointsItem);
                    this.plot1.AddPoints("Разностный метод", Color.Red, points);
                    this.plot1.Draw();
                }
            }
            else if ((string)this.choiceProblemBox.SelectedItem == "Основная задача (дискр. м. времени)")
            {                
                ColorGraphic color = new ColorGraphic();
                List<PointF[]> pointsList = BoundaryValueProblem1D.FiniteDiffMethodForHeatEquation(A, B, items, u1, u2, l1); 
                List<PointF> points = new List<PointF>(pointsList[0]);
                this.plot1.UpdatePoints("t = 0", Color.Black, points);
                this.plot1.Draw();
                for (int j = 1; j < 200; j *= 5)
                {
                    points = new List<PointF>(pointsList[j]);
                    this.plot1.UpdatePoints("t = dt*" + j + " чис. реш.", Color.FromName(color.ToString()), points);
                    color++;
                    this.plot1.Draw();
                }
                points = new List<PointF>(pointsList[pointsList.Count - 1]);
                this.plot1.UpdatePoints("t = tend", Color.Red, points);
                this.plot1.Draw();
            }
        }

        private void plot1_Load(object sender, EventArgs e)
        {

        }
    }
}
