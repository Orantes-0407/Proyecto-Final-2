using System.Data.SqlClient;

namespace Tutor
{
    partial class Form2
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            progressBar1 = new ProgressBar();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Blue;
            label1.Location = new Point(119, 25);
            label1.Name = "label1";
            label1.Size = new Size(519, 34);
            label1.TabIndex = 0;
            label1.Text = "Bienvenido a tu Asistente de Aprendizaje";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(60, 110);
            label2.Name = "label2";
            label2.Size = new Size(133, 26);
            label2.TabIndex = 1;
            label2.Text = "Matemáticas";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Blue;
            label3.Location = new Point(231, 107);
            label3.Name = "label3";
            label3.Size = new Size(260, 26);
            label3.TabIndex = 2;
            label3.Text = "Comunicación y Lenguaje";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(521, 107);
            label4.Name = "label4";
            label4.Size = new Size(218, 26);
            label4.TabIndex = 3;
            label4.Text = "Comprensión Lectora";
            // 
            // button1
            // 
            button1.BackColor = Color.IndianRed;
            button1.Location = new Point(67, 167);
            button1.Name = "button1";
            button1.Size = new Size(118, 44);
            button1.TabIndex = 4;
            button1.Text = "Generar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += buttonGenerar_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.HotTrack;
            button2.ForeColor = SystemColors.ControlText;
            button2.Location = new Point(306, 167);
            button2.Name = "button2";
            button2.Size = new Size(109, 44);
            button2.TabIndex = 5;
            button2.Text = "Generar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.GreenYellow;
            button3.Location = new Point(556, 165);
            button3.Name = "button3";
            button3.Size = new Size(111, 46);
            button3.TabIndex = 6;
            button3.Text = "Generar";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(67, 273);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(600, 51);
            progressBar1.TabIndex = 7;
            progressBar1.Click += progressBar1_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.LawnGreen;
            button4.Location = new Point(672, 392);
            button4.Name = "button4";
            button4.Size = new Size(94, 45);
            button4.TabIndex = 8;
            button4.Text = "Salir";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = Color.Turquoise;
            button5.Location = new Point(44, 394);
            button5.Name = "button5";
            button5.Size = new Size(127, 43);
            button5.TabIndex = 9;
            button5.Text = "Registros";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.BackColor = Color.MediumBlue;
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.Location = new Point(216, 392);
            button6.Name = "button6";
            button6.Size = new Size(179, 43);
            button6.TabIndex = 10;
            button6.Text = "Preguntar a PerriProfe";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(795, 457);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(progressBar1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Button button1;
        private Button button2;
        private Button button3;
        private ProgressBar progressBar1;
        private Button button4;
        private Button button5;
        private Button button6;
    }
}
