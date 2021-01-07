namespace Lab05_01
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.texte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.count = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.plot1 = new IvanovAC.Controls.Plot();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.texte);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.count);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 95);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.25F);
            this.label3.Location = new System.Drawing.Point(312, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Точность для мет. \"предиктор-корректор\"";
            // 
            // texte
            // 
            this.texte.Location = new System.Drawing.Point(379, 34);
            this.texte.Name = "texte";
            this.texte.Size = new System.Drawing.Size(92, 20);
            this.texte.TabIndex = 5;
            this.texte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.25F);
            this.label2.Location = new System.Drawing.Point(60, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Количество элементов сетки";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(97, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Метод решения";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.25F);
            this.button1.Location = new System.Drawing.Point(637, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 46);
            this.button1.TabIndex = 2;
            this.button1.Text = "Решить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Click);
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(100, 69);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(77, 20);
            this.count.TabIndex = 1;
            this.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Простейший метод Эйлера",
            "Исправленный метод Эйлера",
            "Модифицированный метод Эйлера",
            "Все методы и аналит. решение",
            "Метод Рунге-Кутты (3 пр. точ.)",
            "Метод Рунге-Кутты (4 пр. точ.)",
            "Аналитическое решение",
            "Методы Рунге-Кутты (3,4) и аналит. решение",
            "Мет. Эйлера с итерациями и аналит. решение",
            "Мет. двухшаг. прогноза и корр. и аналит. реш.",
            "Мет. двухшаг. прогноза и корр.",
            "Сравнение методов Эйлера",
            "Сравнение методов Эйлера и аналит. реш.",
            "Мет. Рунге-Кутты 3 пр. точ. и аналит. реш.",
            "Мет. Рунге-Кутты 4 пр. точ.",
            "Мет. Рунге-Кутты 4 пр. точ. и аналит. реш.",
            "Мет. Рунге-Кутты 3 и 4 пр. т. и ан. реш.",
            "Мет. Эйлера с итерациями",
            "Мет. Милна и аналит. решение",
            "Метод Милна",
            "Разн. мет. 1-го пор. аппр.",
            "Разн. мет. 2-го пор. аппр.",
            "Разн. мет. 2-го пор. аппр. и аналит. реш.",
            "Разн. мет. 1-го пор. аппр. и аналит. реш.",
            "Метод прогонки",
            "Метод прогонки и аналит. реш.",
            "Разностный мет. и аналит. реш.",
            "Разностный метод"});
            this.comboBox1.Location = new System.Drawing.Point(12, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(246, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // plot1
            // 
            this.plot1.CenterPoint = new System.Drawing.Point(-2147483648, -2147483648);
            this.plot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot1.HeightPictureBox = 335;
            this.plot1.Location = new System.Drawing.Point(0, 95);
            this.plot1.MinimumSize = new System.Drawing.Size(320, 200);
            this.plot1.MinPix = 5;
            this.plot1.Name = "plot1";
            this.plot1.ScaleX = 55511151231.25782D;
            this.plot1.ScaleY = 119200360.43439949D;
            this.plot1.Size = new System.Drawing.Size(800, 355);
            this.plot1.TabIndex = 1;
            this.plot1.WidthPictureBox = 800;
            this.plot1.XName = "X";
            this.plot1.XUnits = "in";
            this.plot1.YName = "Y";
            this.plot1.YUnits = "in";
            this.plot1.Load += new System.EventHandler(this.plot1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.plot1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.ComboBox comboBox1;
        private IvanovAC.Controls.Plot plot1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox texte;
    }
}

