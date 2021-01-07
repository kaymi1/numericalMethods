using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.plot1.CenterPoint = new Point(this.plot1.WidthPictureBox / 2, this.plot1.HeightPictureBox / 2);
            this.plot1.UpdateItem("sin(x)", Color.Red, Math.Sin);
            this.plot1.UpdateItem("cos(x)", Color.Black, Math.Cos);
            this.plot1.UpdatePoints("sinn(x)", Color.Black, new List<PointF>(new PointF[]
                {
                    new PointF((float)-Math.PI, (float)Math.Sin(-Math.PI)),
                    new PointF((float)-Math.PI / 2, (float)Math.Sin(-Math.PI / 2)),
                    new PointF(0, (float)Math.Sin(0)),
                    new PointF((float)Math.PI / 2, (float)Math.Sin(Math.PI / 2)),
                    new PointF((float)Math.PI, (float)Math.Sin(Math.PI))
                }
            ));
            this.plot1.Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void plot1_Load(object sender, EventArgs e)
        {

        }
    }
}
