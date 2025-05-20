
namespace Tutor
{
    partial class MenuInicio
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuInicio));
            label1 = new Label();
            Iniciar = new Button();
            progressBar1 = new ProgressBar();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            label2 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Transparent;
            label1.Location = new Point(38, 150);
            label1.Name = "label1";
            label1.Size = new Size(635, 67);
            label1.TabIndex = 0;
            label1.Text = "Asistente de Aprendizaje";
            // 
            // Iniciar
            // 
            Iniciar.BackColor = SystemColors.ActiveCaption;
            Iniciar.Location = new Point(625, 313);
            Iniciar.Name = "Iniciar";
            Iniciar.Size = new Size(134, 48);
            Iniciar.TabIndex = 1;
            Iniciar.Text = "Iniciar";
            Iniciar.UseVisualStyleBackColor = false;
            Iniciar.Click += Iniciar_Click;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.BackColor = Color.DarkBlue;
            progressBar1.ForeColor = Color.Lime;
            progressBar1.Location = new Point(38, 375);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(532, 48);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 2;
            // 
            // timer2
            // 
            timer2.Interval = 50;
            timer2.Tick += timer2_Tick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(38, 335);
            label2.Name = "label2";
            label2.Size = new Size(477, 26);
            label2.TabIndex = 3;
            label2.Text = "\"El aprendizaje flexible que se adapta a tu vida\"";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Location = new Point(630, 382);
            button1.Name = "button1";
            button1.Size = new Size(128, 41);
            button1.TabIndex = 4;
            button1.Text = "Salir";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // MenuInicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(progressBar1);
            Controls.Add(Iniciar);
            Controls.Add(label1);
            Name = "MenuInicio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += MenuInicio_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button Iniciar;
        private ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private Label label2;
        private Button button1;
    }
}
