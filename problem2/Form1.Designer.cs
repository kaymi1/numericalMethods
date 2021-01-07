namespace problem2
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
            this.count = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.choiceProblemBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.plot1 = new IvanovAC.Controls.Plot();
            this.SuspendLayout();
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(328, 31);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(77, 20);
            this.count.TabIndex = 2;
            this.count.Text = "200";
            this.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(275, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(205, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Количество элементов сетки";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(516, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 42);
            this.button1.TabIndex = 6;
            this.button1.Text = "Решить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Click);
            // 
            // choiceProblemBox
            // 
            this.choiceProblemBox.FormattingEnabled = true;
            this.choiceProblemBox.Items.AddRange(new object[] {
            "Тестовая задача",
            "Основная задача",
            "Тестовая задача (дискр. м. времени)",
            "Основная задача (дискр. м. времени)"});
            this.choiceProblemBox.Location = new System.Drawing.Point(12, 31);
            this.choiceProblemBox.Name = "choiceProblemBox";
            this.choiceProblemBox.Size = new System.Drawing.Size(216, 21);
            this.choiceProblemBox.TabIndex = 7;
            this.choiceProblemBox.Text = "Основная задача";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(72, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Тип задачи";
            // 
            // plot1
            // 
            this.plot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.plot1.CenterPoint = new System.Drawing.Point(610363, 45952);
            this.plot1.HeightPictureBox = 372;
            this.plot1.Location = new System.Drawing.Point(0, 58);
            this.plot1.MinimumSize = new System.Drawing.Size(320, 200);
            this.plot1.MinPix = 5;
            this.plot1.Name = "plot1";
            this.plot1.ScaleX = 3814.697265625D;
            this.plot1.ScaleY = 589.92134791730007D;
            this.plot1.Size = new System.Drawing.Size(800, 392);
            this.plot1.TabIndex = 0;
            this.plot1.WidthPictureBox = 800;
            this.plot1.XName = "X";
            this.plot1.XUnits = "in";
            this.plot1.YName = "Y";
            this.plot1.YUnits = "in";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.choiceProblemBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.count);
            this.Controls.Add(this.plot1);
            this.Name = "Form1";
            this.Text = "Решение краевой задачи для уравнения теплопроводности методом конечных разностей";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private IvanovAC.Controls.Plot plot1;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox choiceProblemBox;
        private System.Windows.Forms.Label label1;
    }
}

