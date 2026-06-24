namespace project
{
    partial class SavingGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavingGame));
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            label5 = new Label();
            label1 = new Label();
            label6 = new Label();
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            progressBar1 = new ProgressBar();
            label7 = new Label();
            progressBar2 = new ProgressBar();
            label8 = new Label();
            progressBar3 = new ProgressBar();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            timer2 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(-13, 686);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(1343, 203);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-13, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1343, 690);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ActiveCaptionText;
            label3.Font = new Font("Segoe UI Black", 17F, FontStyle.Bold | FontStyle.Italic);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(1007, 637);
            label3.Name = "label3";
            label3.Size = new Size(207, 46);
            label3.TabIndex = 14;
            label3.Text = "ДЫХАНИЕ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ActiveCaptionText;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(19, 702);
            label4.Name = "label4";
            label4.Size = new Size(368, 25);
            label4.TabIndex = 15;
            label4.Text = "(Очернение крови -15 | Температура + 0,5)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ActiveCaptionText;
            label2.Font = new Font("Segoe UI Black", 17F, FontStyle.Bold | FontStyle.Italic);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(518, 637);
            label2.Name = "label2";
            label2.Size = new Size(280, 46);
            label2.TabIndex = 13;
            label2.Text = "ТЕМПЕРАТУРА";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ActiveCaptionText;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(518, 702);
            label5.Name = "label5";
            label5.Size = new Size(267, 25);
            label5.TabIndex = 16;
            label5.Text = "(Температура -1 | Дыхание -10)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Segoe UI Black", 17F, FontStyle.Bold | FontStyle.Italic);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(20, 637);
            label1.Name = "label1";
            label1.Size = new Size(367, 46);
            label1.TabIndex = 12;
            label1.Text = "ОЧЕРНЕНИЕ КРОВИ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.ActiveCaptionText;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(954, 702);
            label6.Name = "label6";
            label6.Size = new Size(329, 25);
            label6.TabIndex = 17;
            label6.Text = "(Дыхание +15 | Очернение крови +10)";
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(24, 16, 16);
            button2.FlatAppearance.BorderColor = Color.Black;
            button2.FlatAppearance.BorderSize = 13;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Cascadia Code", 20F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(470, 730);
            button2.Name = "button2";
            button2.Size = new Size(365, 144);
            button2.TabIndex = 18;
            button2.Text = "ОХЛАЖДЕНИЕ";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(24, 16, 16);
            button1.FlatAppearance.BorderColor = Color.Black;
            button1.FlatAppearance.BorderSize = 13;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Cascadia Code", 20F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(23, 730);
            button1.Name = "button1";
            button1.Size = new Size(365, 144);
            button1.TabIndex = 20;
            button1.Text = "СТИМУЛЯЦИЯ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(24, 16, 16);
            button3.FlatAppearance.BorderColor = Color.Black;
            button3.FlatAppearance.BorderSize = 13;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Cascadia Code", 20F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(935, 730);
            button3.Name = "button3";
            button3.Size = new Size(365, 144);
            button3.TabIndex = 21;
            button3.Text = "ИСКУССТВЕННОЕ ДЫХАНИЕ";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = SystemColors.Desktop;
            progressBar1.Location = new Point(777, 152);
            progressBar1.Maximum = 410;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(464, 51);
            progressBar1.TabIndex = 22;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Black;
            label7.Font = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label7.ForeColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(531, 152);
            label7.Name = "label7";
            label7.Size = new Size(222, 46);
            label7.TabIndex = 23;
            label7.Text = "Температура";
            // 
            // progressBar2
            // 
            progressBar2.ForeColor = SystemColors.Desktop;
            progressBar2.Location = new Point(777, 50);
            progressBar2.Maximum = 80;
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(464, 51);
            progressBar2.TabIndex = 24;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Black;
            label8.Font = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label8.ForeColor = SystemColors.ButtonHighlight;
            label8.Location = new Point(450, 50);
            label8.Name = "label8";
            label8.Size = new Size(303, 46);
            label8.TabIndex = 25;
            label8.Text = "Очернение крови";
            // 
            // progressBar3
            // 
            progressBar3.ForeColor = SystemColors.Desktop;
            progressBar3.Location = new Point(777, 255);
            progressBar3.Minimum = 20;
            progressBar3.Name = "progressBar3";
            progressBar3.Size = new Size(464, 51);
            progressBar3.TabIndex = 26;
            progressBar3.Value = 20;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Black;
            label9.Font = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label9.ForeColor = SystemColors.ButtonHighlight;
            label9.Location = new Point(572, 255);
            label9.Name = "label9";
            label9.Size = new Size(159, 46);
            label9.TabIndex = 27;
            label9.Text = "Дыхание";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Black;
            label10.Font = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label10.ForeColor = SystemColors.ButtonHighlight;
            label10.Location = new Point(1252, 255);
            label10.Name = "label10";
            label10.Size = new Size(48, 46);
            label10.TabIndex = 28;
            label10.Text = "%";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Black;
            label11.Font = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label11.ForeColor = SystemColors.ButtonHighlight;
            label11.Location = new Point(1252, 152);
            label11.Name = "label11";
            label11.Size = new Size(54, 46);
            label11.TabIndex = 29;
            label11.Text = "°C";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Black;
            label12.Font = new Font("Segoe UI", 17F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label12.ForeColor = SystemColors.ButtonHighlight;
            label12.Location = new Point(1252, 50);
            label12.Name = "label12";
            label12.Size = new Size(48, 46);
            label12.TabIndex = 30;
            label12.Text = "%";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.FromArgb(23, 23, 23);
            label13.Font = new Font("Segoe UI Black", 50F, FontStyle.Bold | FontStyle.Italic);
            label13.ForeColor = SystemColors.ButtonHighlight;
            label13.Location = new Point(23, 82);
            label13.Name = "label13";
            label13.Size = new Size(178, 133);
            label13.TabIndex = 31;
            label13.Text = "60";
            // 
            // timer2
            // 
            timer2.Interval = 1000;
            timer2.Tick += timer2_Tick;
            // 
            // SavingGame
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1329, 886);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(progressBar3);
            Controls.Add(label8);
            Controls.Add(progressBar2);
            Controls.Add(label7);
            Controls.Add(progressBar1);
            Controls.Add(button3);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(label6);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Font = new Font("Segoe UI Black", 9F, FontStyle.Bold | FontStyle.Italic);
            Name = "SavingGame";
            Text = "СПАСИ ЕГО";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label3;
        private Label label4;
        private Label label2;
        private Label label5;
        private Label label1;
        private Label label6;
        private Button button2;
        private Button button1;
        private Button button3;
        private ProgressBar progressBar1;
        private Label label7;
        private ProgressBar progressBar2;
        private Label label8;
        private ProgressBar progressBar3;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private System.Windows.Forms.Timer timer2;
    }
}
