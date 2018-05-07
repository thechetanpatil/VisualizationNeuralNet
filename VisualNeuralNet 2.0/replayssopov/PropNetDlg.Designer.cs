namespace replayssopov
{
    partial class PropNetDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NumberOfInputs = new System.Windows.Forms.TextBox();
            this.NumberOfNeyrons = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NumberOfLayers = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NumberOfOutputs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Cancel_button = new System.Windows.Forms.Button();
            this.OK_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NumberOfInputs
            // 
            this.NumberOfInputs.Location = new System.Drawing.Point(19, 42);
            this.NumberOfInputs.Name = "NumberOfInputs";
            this.NumberOfInputs.Size = new System.Drawing.Size(75, 20);
            this.NumberOfInputs.TabIndex = 4;
            this.NumberOfInputs.Text = "1";
            // 
            // NumberOfNeyrons
            // 
            this.NumberOfNeyrons.Location = new System.Drawing.Point(19, 193);
            this.NumberOfNeyrons.Name = "NumberOfNeyrons";
            this.NumberOfNeyrons.Size = new System.Drawing.Size(75, 20);
            this.NumberOfNeyrons.TabIndex = 7;
            this.NumberOfNeyrons.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "NumberOfInputs";
            // 
            // NumberOfLayers
            // 
            this.NumberOfLayers.Location = new System.Drawing.Point(19, 142);
            this.NumberOfLayers.Name = "NumberOfLayers";
            this.NumberOfLayers.Size = new System.Drawing.Size(75, 20);
            this.NumberOfLayers.TabIndex = 6;
            this.NumberOfLayers.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "NumberOfOutputs";
            // 
            // NumberOfOutputs
            // 
            this.NumberOfOutputs.Location = new System.Drawing.Point(19, 90);
            this.NumberOfOutputs.Name = "NumberOfOutputs";
            this.NumberOfOutputs.Size = new System.Drawing.Size(75, 20);
            this.NumberOfOutputs.TabIndex = 5;
            this.NumberOfOutputs.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "NumberOfLayers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "NumberOfNeyrons";
            // 
            // Cancel_button
            // 
            this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_button.Location = new System.Drawing.Point(101, 318);
            this.Cancel_button.Name = "Cancel_button";
            this.Cancel_button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_button.TabIndex = 13;
            this.Cancel_button.Text = "Cancel";
            this.Cancel_button.UseVisualStyleBackColor = true;
            // 
            // OK_button
            // 
            this.OK_button.Location = new System.Drawing.Point(101, 281);
            this.OK_button.Name = "OK_button";
            this.OK_button.Size = new System.Drawing.Size(75, 23);
            this.OK_button.TabIndex = 12;
            this.OK_button.Text = "OK";
            this.OK_button.UseVisualStyleBackColor = true;
            this.OK_button.Click += new System.EventHandler(this.OK_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NumberOfInputs);
            this.groupBox1.Controls.Add(this.NumberOfNeyrons);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.NumberOfLayers);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.NumberOfOutputs);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(82, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 241);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "NetProperties";
            // 
            // PropNetDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 365);
            this.Controls.Add(this.Cancel_button);
            this.Controls.Add(this.OK_button);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PropNetDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки нейронной сети";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox NumberOfInputs;
        private System.Windows.Forms.TextBox NumberOfNeyrons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NumberOfLayers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NumberOfOutputs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Cancel_button;
        private System.Windows.Forms.Button OK_button;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}