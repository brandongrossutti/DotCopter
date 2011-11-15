namespace DotCopter.GroundControl
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxYaw = new System.Windows.Forms.TextBox();
            this.textBoxRoll = new System.Windows.Forms.TextBox();
            this.textBoxPitch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPitchProportional = new System.Windows.Forms.TextBox();
            this.textBoxPitchIntegral = new System.Windows.Forms.TextBox();
            this.textBoxPitchDerivative = new System.Windows.Forms.TextBox();
            this.buttonPitchProportional = new System.Windows.Forms.Button();
            this.buttonPitchIntegral = new System.Windows.Forms.Button();
            this.buttonPitchDerivative = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(234, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(17, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxYaw);
            this.groupBox1.Controls.Add(this.textBoxRoll);
            this.groupBox1.Controls.Add(this.textBoxPitch);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 141);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accelerometer";
            // 
            // textBoxYaw
            // 
            this.textBoxYaw.Location = new System.Drawing.Point(6, 110);
            this.textBoxYaw.Name = "textBoxYaw";
            this.textBoxYaw.Size = new System.Drawing.Size(285, 20);
            this.textBoxYaw.TabIndex = 5;
            // 
            // textBoxRoll
            // 
            this.textBoxRoll.Location = new System.Drawing.Point(6, 71);
            this.textBoxRoll.Name = "textBoxRoll";
            this.textBoxRoll.Size = new System.Drawing.Size(285, 20);
            this.textBoxRoll.TabIndex = 4;
            // 
            // textBoxPitch
            // 
            this.textBoxPitch.Location = new System.Drawing.Point(6, 32);
            this.textBoxPitch.Name = "textBoxPitch";
            this.textBoxPitch.Size = new System.Drawing.Size(285, 20);
            this.textBoxPitch.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Yaw";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Roll";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pitch";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonPitchDerivative);
            this.groupBox2.Controls.Add(this.buttonPitchIntegral);
            this.groupBox2.Controls.Add(this.buttonPitchProportional);
            this.groupBox2.Controls.Add(this.textBoxPitchDerivative);
            this.groupBox2.Controls.Add(this.textBoxPitchIntegral);
            this.groupBox2.Controls.Add(this.textBoxPitchProportional);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 92);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Pitch Proportional";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Pitch Integral";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Pitch Derivative";
            // 
            // textBoxPitchProportional
            // 
            this.textBoxPitchProportional.Location = new System.Drawing.Point(6, 32);
            this.textBoxPitchProportional.Name = "textBoxPitchProportional";
            this.textBoxPitchProportional.Size = new System.Drawing.Size(90, 20);
            this.textBoxPitchProportional.TabIndex = 6;
            // 
            // textBoxPitchIntegral
            // 
            this.textBoxPitchIntegral.Location = new System.Drawing.Point(102, 32);
            this.textBoxPitchIntegral.Name = "textBoxPitchIntegral";
            this.textBoxPitchIntegral.Size = new System.Drawing.Size(90, 20);
            this.textBoxPitchIntegral.TabIndex = 7;
            // 
            // textBoxPitchDerivative
            // 
            this.textBoxPitchDerivative.Location = new System.Drawing.Point(198, 32);
            this.textBoxPitchDerivative.Name = "textBoxPitchDerivative";
            this.textBoxPitchDerivative.Size = new System.Drawing.Size(88, 20);
            this.textBoxPitchDerivative.TabIndex = 8;
            // 
            // buttonPitchProportional
            // 
            this.buttonPitchProportional.Location = new System.Drawing.Point(5, 58);
            this.buttonPitchProportional.Name = "buttonPitchProportional";
            this.buttonPitchProportional.Size = new System.Drawing.Size(88, 23);
            this.buttonPitchProportional.TabIndex = 9;
            this.buttonPitchProportional.Text = "Update";
            this.buttonPitchProportional.UseVisualStyleBackColor = true;
            this.buttonPitchProportional.Click += new System.EventHandler(this.buttonPitchProportional_Click);
            // 
            // buttonPitchIntegral
            // 
            this.buttonPitchIntegral.Location = new System.Drawing.Point(102, 58);
            this.buttonPitchIntegral.Name = "buttonPitchIntegral";
            this.buttonPitchIntegral.Size = new System.Drawing.Size(88, 23);
            this.buttonPitchIntegral.TabIndex = 10;
            this.buttonPitchIntegral.Text = "Update";
            this.buttonPitchIntegral.UseVisualStyleBackColor = true;
            this.buttonPitchIntegral.Click += new System.EventHandler(this.buttonPitchIntegral_Click);
            // 
            // buttonPitchDerivative
            // 
            this.buttonPitchDerivative.Location = new System.Drawing.Point(198, 58);
            this.buttonPitchDerivative.Name = "buttonPitchDerivative";
            this.buttonPitchDerivative.Size = new System.Drawing.Size(88, 23);
            this.buttonPitchDerivative.TabIndex = 11;
            this.buttonPitchDerivative.Text = "Update";
            this.buttonPitchDerivative.UseVisualStyleBackColor = true;
            this.buttonPitchDerivative.Click += new System.EventHandler(this.buttonPitchDerivative_Click);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(315, 17);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Pitch Gyro";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "Pitch Radio";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(466, 261);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 290);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxYaw;
        private System.Windows.Forms.TextBox textBoxRoll;
        private System.Windows.Forms.TextBox textBoxPitch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxPitchDerivative;
        private System.Windows.Forms.TextBox textBoxPitchIntegral;
        private System.Windows.Forms.TextBox textBoxPitchProportional;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonPitchDerivative;
        private System.Windows.Forms.Button buttonPitchIntegral;
        private System.Windows.Forms.Button buttonPitchProportional;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;

    }
}

