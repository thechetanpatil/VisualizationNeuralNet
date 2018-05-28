namespace replayssopov
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxRTi = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbIsVisualization = new System.Windows.Forms.CheckBox();
            this.PropertiesNet_button = new System.Windows.Forms.Button();
            this.StandardF = new System.Windows.Forms.Button();
            this.BOpenVNN = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Start_button = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CurrentItBox = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LearnErBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBoxRTi);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(203, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 320);
            this.panel1.TabIndex = 40;
            // 
            // checkBoxRTi
            // 
            this.checkBoxRTi.AutoSize = true;
            this.checkBoxRTi.Location = new System.Drawing.Point(219, 301);
            this.checkBoxRTi.Name = "checkBoxRTi";
            this.checkBoxRTi.Size = new System.Drawing.Size(114, 17);
            this.checkBoxRTi.TabIndex = 55;
            this.checkBoxRTi.Text = "Real Time Imagine";
            this.checkBoxRTi.UseVisualStyleBackColor = true;
            this.checkBoxRTi.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(351, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 19);
            this.button2.TabIndex = 54;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightCyan;
            this.chart1.BackImageTransparentColor = System.Drawing.Color.Silver;
            this.chart1.BackSecondaryColor = System.Drawing.Color.White;
            this.chart1.BorderlineColor = System.Drawing.Color.LightBlue;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            this.chart1.BorderlineWidth = 2;
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Blue;
            series1.Legend = "Legend1";
            series1.Name = "MyChart";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "PointChart";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(559, 320);
            this.chart1.TabIndex = 34;
            this.chart1.Text = "chart2";
            this.chart1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbIsVisualization);
            this.panel2.Controls.Add(this.PropertiesNet_button);
            this.panel2.Controls.Add(this.StandardF);
            this.panel2.Controls.Add(this.BOpenVNN);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.Start_button);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.CurrentItBox);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.LearnErBox);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(203, 320);
            this.panel2.TabIndex = 41;
            // 
            // cbIsVisualization
            // 
            this.cbIsVisualization.AutoSize = true;
            this.cbIsVisualization.Location = new System.Drawing.Point(92, 175);
            this.cbIsVisualization.Name = "cbIsVisualization";
            this.cbIsVisualization.Size = new System.Drawing.Size(84, 17);
            this.cbIsVisualization.TabIndex = 56;
            this.cbIsVisualization.Text = "Visualization";
            this.cbIsVisualization.UseVisualStyleBackColor = true;
            // 
            // PropertiesNet_button
            // 
            this.PropertiesNet_button.Enabled = false;
            this.PropertiesNet_button.Location = new System.Drawing.Point(17, 37);
            this.PropertiesNet_button.Name = "PropertiesNet_button";
            this.PropertiesNet_button.Size = new System.Drawing.Size(170, 24);
            this.PropertiesNet_button.TabIndex = 40;
            this.PropertiesNet_button.Text = "Neural net configuration";
            this.PropertiesNet_button.UseVisualStyleBackColor = true;
            this.PropertiesNet_button.Click += new System.EventHandler(this.PropertiesNet_button_Click);
            // 
            // StandardF
            // 
            this.StandardF.Location = new System.Drawing.Point(15, 7);
            this.StandardF.Name = "StandardF";
            this.StandardF.Size = new System.Drawing.Size(172, 24);
            this.StandardF.TabIndex = 55;
            this.StandardF.Text = "Function";
            this.StandardF.UseVisualStyleBackColor = true;
            this.StandardF.Click += new System.EventHandler(this.StandardF_Click);
            // 
            // BOpenVNN
            // 
            this.BOpenVNN.Location = new System.Drawing.Point(12, 202);
            this.BOpenVNN.Name = "BOpenVNN";
            this.BOpenVNN.Size = new System.Drawing.Size(178, 38);
            this.BOpenVNN.TabIndex = 54;
            this.BOpenVNN.Text = "Open visualization from file";
            this.BOpenVNN.UseVisualStyleBackColor = true;
            this.BOpenVNN.Click += new System.EventHandler(this.BOpenVNN_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 173);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(69, 20);
            this.textBox2.TabIndex = 53;
            this.textBox2.Visible = false;
            // 
            // Start_button
            // 
            this.Start_button.Enabled = false;
            this.Start_button.Location = new System.Drawing.Point(12, 257);
            this.Start_button.Name = "Start_button";
            this.Start_button.Size = new System.Drawing.Size(178, 39);
            this.Start_button.TabIndex = 52;
            this.Start_button.Text = "Start learning";
            this.Start_button.UseVisualStyleBackColor = true;
            this.Start_button.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 152);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "LearningError";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 48;
            this.label6.Text = "CurrentIteration";
            // 
            // CurrentItBox
            // 
            this.CurrentItBox.Location = new System.Drawing.Point(13, 126);
            this.CurrentItBox.Name = "CurrentItBox";
            this.CurrentItBox.Size = new System.Drawing.Size(69, 20);
            this.CurrentItBox.TabIndex = 47;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(121, 86);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(69, 20);
            this.textBox3.TabIndex = 46;
            this.textBox3.Text = "0,1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(118, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Learning rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "FinalError";
            // 
            // LearnErBox
            // 
            this.LearnErBox.Enabled = false;
            this.LearnErBox.Location = new System.Drawing.Point(121, 126);
            this.LearnErBox.Name = "LearnErBox";
            this.LearnErBox.Size = new System.Drawing.Size(69, 20);
            this.LearnErBox.TabIndex = 42;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(13, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(73, 20);
            this.textBox1.TabIndex = 41;
            this.textBox1.Text = "1000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "EpochCount";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 320);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Neural Net Demonstration";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBoxRTi;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbIsVisualization;
        private System.Windows.Forms.Button PropertiesNet_button;
        private System.Windows.Forms.Button StandardF;
        private System.Windows.Forms.Button BOpenVNN;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Start_button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox CurrentItBox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LearnErBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;

    }
}

