namespace Lab02_01
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.plot1 = new IvanovAC.Controls.Plot();
            this.SuspendLayout();
            // 
            // plot1
            // 
            this.plot1.CenterPoint = new System.Drawing.Point(437, 253);
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.HeightPictureBox = 507;
            this.plot1.Location = new System.Drawing.Point(0, 0);
            this.plot1.MinimumSize = new System.Drawing.Size(320, 200);
            this.plot1.MinPix = 5;
            this.plot1.Name = "plot1";
            this.plot1.ScaleX = 109.25000190734863D;
            this.plot1.ScaleY = 112.66666412353516D;
            this.plot1.Size = new System.Drawing.Size(874, 527);
            this.plot1.TabIndex = 0;
            this.plot1.WidthPictureBox = 874;
            this.plot1.XName = "X";
            this.plot1.XUnits = "cm";
            this.plot1.YName = "Y";
            this.plot1.YUnits = "cm";
            this.plot1.Load += new System.EventHandler(this.plot1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 527);
            this.Controls.Add(this.plot1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private IvanovAC.Controls.Plot plot1;
    }
}

